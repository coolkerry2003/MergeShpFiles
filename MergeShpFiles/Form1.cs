using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PilotGaea.TMPEngine;
using System.Runtime.InteropServices;
using System.IO;
using PilotGaea.Geometry;

namespace MergeShpFiles
{
    public partial class Form1 : Form
    {
        CServer Server = null;
        bool InitStatus = false;
        Dictionary<string, FIELD_TYPE> UniFields = new Dictionary<string, FIELD_TYPE>();
        List<string> SHPFileNames = new List<string>();
        public Form1()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(@"C:\Program Files\PilotGaea\TileMap");
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        private void Form1_Shown(object sender, EventArgs e)
        {
            Server = new CServer();
            int ServerPort = 8081;
            string TMPXPath = @"C:\ProgramData\PilotGaea\PGMaps\地圖伺服器#01\Map.TMPX";
            string PluginPath = @"C:\Program Files\PilotGaea\TileMap\plugins";
            InitStatus = Server.Init(ServerPort, TMPXPath, PluginPath);
            if (!InitStatus)
            {
                MessageBox.Show("Init Failed!!!");
            }
            AllocConsole();
        }

        private void btn_OpenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tb_SourceFolder.Text = dialog.SelectedPath;                
                string[] wholeSHPFileNames = Directory.GetFiles(tb_SourceFolder.Text, "*.shp", SearchOption.AllDirectories);
                Console.WriteLine("合併SHP欄位:");
                for (int i = 0; i < wholeSHPFileNames.Length; i++)
                {
                    string fileName = wholeSHPFileNames[i];
                    SHPFileNames.Add(fileName);
                    string[] fields;
                    FIELD_TYPE[] types;
                    getFieldNamesFromSHP(fileName, out fields, out types);
                    if (fields == null || types == null)
                        continue;

                    for (int j = 0; j < fields.Length; j++)
                    {
                        string field = fields[j].ToUpper();
                        if (!UniFields.ContainsKey(field))
                            UniFields.Add(field, types[j]);
                    }
                    Console.WriteLine(wholeSHPFileNames[i]);
                }
            }
        }

        private void getFieldNamesFromSHP(string fileName, out string[] fieldNames, out FIELD_TYPE[] fieldTypes)
        {
            fieldNames = null;
            fieldTypes = null;
            CSHPFile input = Server.GeoDB.CreateSHPFile();
            if (input.Open(fileName))
            {
                fieldNames = new string[input.FieldDefines.Count];
                fieldTypes = new FIELD_TYPE[input.FieldDefines.Count];
                for (int i = 0; i < input.FieldDefines.Count; i++)
                {
                    fieldNames[i] = input.FieldDefines[i].Name;
                    fieldTypes[i] = input.FieldDefines[i].Type;
                }
            }
            input.Close();
        }

        private void btn_ExportMergeResult_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.RestoreDirectory = false;
            dialog.DefaultExt = ".shp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                CSHPFile output = Server.GeoDB.CreateSHPFile();
                string[] fields = UniFields.Select(field => field.Key).ToArray();
                FIELD_TYPE[] types = UniFields.Select(type => type.Value).ToArray();
                int[] lengths = new int[fields.Length];
                for (int i = 0; i < lengths.Length; i++)
                {
                    lengths[i] = 255;
                }
                if (output.Create(fields, types, lengths))
                {
                    for (int i = 0; i < SHPFileNames.Count; i++)
                    {
                        CSHPFile input = Server.GeoDB.CreateSHPFile();
                        if (input.Open(SHPFileNames[i]))
                        {
                            int[] indices = getFieldIndexInUniFields(input.FieldDefines, fields);
                            CSHPEntityCollection entities = input.GetEntities();
                            createEntities(output, entities, indices);
                        }
                        input.Close();
                    }
                }
                if (output.Save(dialog.FileName))
                {
                    Console.WriteLine("匯出完成");
                }

                output.Close();
            }  
        }

        private int[] getFieldIndexInUniFields(CFieldDefineCollection fields, string[] uniFields)
        {
            int[] indices = new int[fields.Count];
            for (int i = 0; i < indices.Length; i++)
            {
                int index = Array.IndexOf(uniFields, fields[i].Name.ToUpper());
                indices[i] = index;
            }
            return indices;
        }

        private void createEntities(CSHPFile file, CSHPEntityCollection entities, int[] indices)
        {
            
            for (int i = 0; i < entities.Count; i++)
            {
                CSHPEntity entity = entities[i];
                GeoPoint p = null;
                GeoPolyline pl = null;
                GeoPolygonSet pgs = null;
                switch (entity.Type)
                {
                    case GeoType.POINT:
                        entity.GetGeo(ref p);
                        break;
                    case GeoType.LINE:
                        entity.GetGeo(ref pl);
                        break;
                    case GeoType.AREA:
                        entity.GetGeo(ref pgs);
                        break;
                }
                string[] entityAttr = entity.GetAttrs();
                string[] attributes = new string[UniFields.Count];
                for (int j = 0; j < attributes.Length; j++)
                {
                    attributes[j] = "";
                }
                for (int j = 0; j < entityAttr.Length; j++)
                {
                    int index = indices[j];
                    if (index != -1)
                        attributes[index] = entityAttr[j];
                }
               

                if (p != null)
                    file.CreateEntity(p, attributes);
                else if (pl != null)
                    file.CreateEntity(pl, attributes);
                else if (pgs != null)
                    file.CreateEntity(pgs, attributes);
            }
            
        }
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace UmaUmaEditor
{
    public partial class UmaUmaEditor : Form
    {
        UmaUmaData data;

        public UmaUmaEditor()
        {
            InitializeComponent();
            Initialize();
        }

        void Initialize()
        {
            // JSON�I�v�V�����ݒ�
            var options = new JsonSerializerOptions
            {
                // ���{���ϊ����邽�߂̃G���R�[�h�ݒ�
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),

                // �C���f���g��t����
                WriteIndented = true
            };

            //UmaLibrary/UmaMusumeLibrary.json��ǂݍ���
            string str;
            using (StreamReader sr = new StreamReader("UmaLibrary/UmaMusumeLibrary.json"))
            {
                str = sr.ReadToEnd();
            }

            //�f�V���A���C�Y
            data = JsonSerializer.Deserialize<UmaUmaData>(str, options);

            //�\���p�ɃA�C�e���ǉ�
            cb1.Items.Add("Charactor");
            cb1.Items.Add("Support");

            //�ŏ��̗v�f��I����Ԃ�
            cb1.SelectedIndex = 0;

            UpdateCB2();
            UpdateCB3();
            UpdateCB4();
        }

        void UpdateCB2()
        {
            //�܂����ׂăN���A
            cb2.Items.Clear();

            //�����N���
            //Charactor
            if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
            {
                foreach (var v in data.charas)
                {
                    cb2.Items.Add(v.Key);
                }
            }
            //Support
            else if (cb1.SelectedIndex == (int)Kinds.KIND_SUPPORT)
            {
                foreach (var v in data.supports)
                {
                    cb2.Items.Add(v.Key);
                }
            }

            //�ŏ��̗v�f��I����Ԃ�
            cb2.SelectedIndex = 0;
        }

        void UpdateCB3()
        {
            //�܂����ׂăN���A
            cb3.Items.Clear();

            //�J�[�h
            //Charactor
            if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
            {
                foreach (var v in data.charas[cb2.Text])
                {
                    cb3.Items.Add(v.Key);
                }
            }
            //Support
            else if (cb1.SelectedIndex == (int)Kinds.KIND_SUPPORT)
            {
                foreach (var v in data.supports[cb2.Text])
                {
                    cb3.Items.Add(v.Key);
                }
            }

            //�ŏ��̗v�f��I����Ԃ�
            cb3.SelectedIndex = 0;
        }

        void UpdateCB4()
        {
            //�܂����ׂăN���A
            cb4.Items.Clear();

            //�C�x���g���X�g
            //Charactor
            if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
            {
                foreach (var v1 in data.charas[cb2.Text][cb3.Text].events)
                {
                    foreach (var v2 in v1.Keys)
                    {
                        cb4.Items.Add(v2);
                    }
                }
            }
            //Support
            else if (cb1.SelectedIndex == (int)Kinds.KIND_SUPPORT)
            {
                foreach (var v1 in data.supports[cb2.Text][cb3.Text].events)
                {
                    foreach (var v2 in v1.Keys)
                    {
                        cb4.Items.Add(v2);
                    }
                }
            }

            //�ŏ��̗v�f��I����Ԃ�
            cb4.SelectedIndex = 0;
        }

        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB2();
            UpdateCB3();
            UpdateCB4();
        }

        private void cb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB3();
            UpdateCB4();
        }

        private void cb3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB4();
        }
    }
}
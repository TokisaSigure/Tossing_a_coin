using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Kinect;

namespace WpfApplication1.CLass
{
    /// <summary>
    /// キネクトに必要な変数の宣言等
    /// 基本的にはBody関連のみ取り扱うつもり
    /// じゃんけん用の処理とかついてるけど、あくまでおまけなんだからね！・・・おまけってことにしてください（切実
    /// </summary>
    class KinectUtil
    {
        /// <summary>
        /// カメラ空間のZ軸が負数にならないための定数
        /// </summary>
        private const float InferredZPositionClamp = 0.1f;
        
        /// <summary>
        /// キネクトの有無チェック
        /// </summary>
        private KinectSensor kinectSensor = null;
        
        /// <summary>
        /// body frameの読み込み器
        /// </summary>
        private BodyFrameReader bodyFrameReader = null;
        
        /// <summary>
        /// Bodyデータを格納するためのArray配列
        /// </summary>
        private Body[] bodies = null;

        /// <summary>
        /// ボーンの定義
        /// </summary>
        private List<Tuple<JointType, JointType>> bones;

        /// <summary>
        /// ボディ情報を格納するための変数
        /// </summary>
        Body dummyBody = null;

        /*---------------------------------------------------------------
        以下、ゲッタ、セッタの宣言
        ---------------------------------------------------------------*/

        public float getInferredZpositionClamp() { return InferredZPositionClamp; } 

        public KinectSensor KS
        {
            set { this.kinectSensor = value; }
            get { return kinectSensor; }
        }
        public BodyFrameReader BFR
        {
            set { this.bodyFrameReader = value; }
            get { return bodyFrameReader; }
        }
        public Body[] Bodies
        {
            set { this.bodies = value; }
            get { return bodies; }
        }
        public List<Tuple<JointType,JointType>>Bones
        {
            set { this.bones = value; }
            get { return bones; }
        }
        public Body DummyBudy
        {
            set { this.dummyBody = value; }
            get { return dummyBody; }
        }

        /*-----------------------------------------------------------
        以下、テストメソッド
        -----------------------------------------------------------*/

        public void kinectInitializeComponent()
        {
            ///キネクト本体の接続を確保、たしか接続されてない場合はfalseとかになった記憶
            kinectSensor = KinectSensor.GetDefault();
            //キネクト開始
            kinectSensor.Open();
            ///bodyを格納するための配列作成
            bodies = new Body[kinectSensor.BodyFrameSource.BodyCount];
            ///ボディリーダーを開く
            bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();
        }

        public void bodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;

            using (var bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame == null)
                {
                    return;
                }
                else
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }

                if (dataReceived)
                {
                    // ボディデータを取得する
                    bodyFrame.GetAndRefreshBodyData(bodies);
                    foreach (var body in bodies.Where(b => b.IsTracked))
                    {
                        DummyBudy = body;
                    }
                    /*
                    //認識しているBodyに対して
                    foreach (var body in bodies.Where(b => b.IsTracked))
                    {
                        dummyBody = body;
                        if (body.HandRightState == HandState.Closed)
                        {
                            System.Diagnostics.Debug.WriteLine("グー");
                        }
                        if (body.HandRightState == HandState.Open)
                        {
                            System.Diagnostics.Debug.WriteLine("パー");
                        }
                        if (body.HandRightState == HandState.Lasso)
                        {
                            System.Diagnostics.Debug.WriteLine("チョキ");
                        }
                    }*/
                }
            }
        }

        public void Kinect_Close()
        {
            bodyFrameReader.Dispose();
            kinectSensor.Close();
        }
    }
}

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

        /*-----------------------------------------------------------
        以下、テストメソッド
        -----------------------------------------------------------*/

        public void kinectInitializeComponent()
        {
            ///キネクト本体の接続を確保、たしか接続されてない場合はfalseとかになった記憶
            kinectSensor = KinectSensor.GetDefault();
            ///bodyを格納するための配列作成
            bodies = new Body[kinectSensor.BodyFrameSource.BodyCount];
            ///ボディリーダーを開く
            bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();
            bodyFrameReader.FrameArrived += bodyFrameReader_FrameArrived;
        }

        private void bodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
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
                    foreach (Body body in this.bodies)
                    {
                        if (body.IsTracked)
                        {
                            IReadOnlyDictionary<JointType, Joint> joints = body.Joints;
                            // convert the joint points to depth (display) space
                            // ジョイントポイントを深度（ディスプレイ）スペースに変換
                            Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                            foreach (JointType jointType in joints.Keys)
                            {
                                // sometimes the depth(Z) of an inferred joint may show as negative
                                // 時々推測されるjointの深さが負として表示される場合があります
                                // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                                // 0.1フレーム前のcoordinatemapperに戻り、チェックする(負数の防止？)
                                CameraSpacePoint position = joints[jointType].Position;
                                if (position.Z < 0)
                                {
                                    position.Z = InferredZPositionClamp;
                                }
                            }
                        }
                    }

                    // ボディデータを取得する
                    bodyFrame.GetAndRefreshBodyData(bodies);
                    //認識しているBodyに対して
                    foreach (var body in bodies.Where(b => b.IsTracked))
                    {
                        //左手のX座標を取得
//                        System.Diagnostics.Debug.WriteLine("X=" + body.Joints[JointType.HandLeft].Position.X);
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
                    }
                }
            }
        }
    }
}

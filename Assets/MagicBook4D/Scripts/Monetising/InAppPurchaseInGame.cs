using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Firecoals.Purchasing
{
    public class InAppPurchaseInGame : IStoreListener
    {
        public static IStoreController m_StoreController;
        // The Unity Purchasing system.
        public static IExtensionProvider m_StoreExtensionProvider;
        // The store-specific Purchasing subsystems.

        public static InAppPurchaseInGame instance;

        public static event Action IAPOnPurchaseFailed = delegate { };
        public static event Action IAPOnPurchaseSuccess = delegate { };
        public static event Action IAPOnInitSuccess = delegate { };
        public static event Action<string> IAPOnInitFailed = delegate { };

        private const int ID_ANIMAL = 1;
        private const int ID_SPACE = 2;
        private const int ID_COLOR = 3;

        public static string ProductID_Animal_Android = "magicbook4d.animal.149";
        public static string ProductID_Animal_iOS = "magicalanimal.license149";
        public static string ProductID_Space_Android = "magicbook4d.space.149";
        public static string ProductID_Space_iOS = "mb02_space_license149";
        public static string ProductID_Color_Android = "magicbook4d.coloring.69";
        public static string ProductID_Color_iOS = "magiccolor_license69";

        public static string ProductID = "";

        ///<summary>
        ///Setup something. call this first before use.
        ///Project_Id: Animal = 1, Sapce = 2, Color = 3
        ///</summary>
        public static void SetUPIAP(int Project_Id)
        {
#if UNITY_ANDROID
            switch (Project_Id)
            {
                case ID_ANIMAL:
                    ProductID = ProductID_Animal_Android;
                    break;
                case ID_SPACE:
                    ProductID = ProductID_Space_Android;
                    break;
                case ID_COLOR:
                    ProductID = ProductID_Color_Android;
                    break;
            }
#endif

#if UNITY_IOS
     switch (Project_Id)
        {
            case ID_ANIMAL:
                ProductID = ProductID_Animal_iOS;
                break;
            case ID_SPACE:
                ProductID = ProductID_Space_iOS;
                break;
            case ID_COLOR:
                ProductID = ProductID_Color_iOS;
                break;
        }
#endif
        }

        //InitIAP
        public static void InitIAP()
        {
            if (instance == null)
            {
                instance = new InAppPurchaseInGame();
            }

            if (m_StoreController == null)
            {
                InitializePurchasing();
            }

        }

        public static bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        //button buy license
        public static void BuyLicense()
        {
            //		Debug.Log ("BuyLicense");
            BuyProductID();
        }

        //Restore purchase for IOS
        public static void RestorePurchases()
        {
            Debug.Log(IsInitialized());
            // If Purchasing has not yet been set up ...
            if (!IsInitialized())
            {
                // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
                //			Debug.Log ("RestorePurchases FAIL. Not initialized.");
                InitIAP();
                //			return;
            }

            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                // ... begin restoring purchases
                // Fetch the Apple store-specific subsystem.
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
                // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
                apple.RestoreTransactions((result) =>
                {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                    // no purchases are available to be restored.
                    if (result)
                    {
                        // This does not mean anything was restored,
                        // merely that the restoration process succeeded.
                        Debug.Log("restoration process succeeded.");
                    }
                    else
                    {
                        // Restoration failed.
                        Debug.Log("restoration process failed");
                    }
                });
            }
            // Otherwise ...
            else
            {

                // We are not running on an Apple device. No work is necessary to restore purchases.
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);

            }
        }

        private static void InitializePurchasing()
        {
            //	Debug.Log ("InitializePurchasing");
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                //			Debug.Log ("IsInitialized");
                return;
            }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
#if UNITY_ANDROID
            builder.AddProduct(ProductID_Animal_Android, ProductType.NonConsumable);
            builder.AddProduct(ProductID_Space_Android, ProductType.NonConsumable);
            builder.AddProduct(ProductID_Color_Android, ProductType.NonConsumable);
#endif

#if UNITY_IOS
        builder.AddProduct(ProductID_Animal_iOS, ProductType.NonConsumable);
        builder.AddProduct(ProductID_Space_iOS, ProductType.NonConsumable);
        builder.AddProduct(ProductID_Color_iOS, ProductType.NonConsumable);
#endif

            UnityPurchasing.Initialize(instance, builder);
            //		Debug.Log ("Initialize  complete");

        }

        private static void BuyProductID()
        {
            //		Debug.Log ("BuyProductID");

            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the ProproductIdduct reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(ProductID);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {

                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }




        //
        // --- IStoreListener
        //

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");
            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features. 
            m_StoreExtensionProvider = extensions;
            //IAPOnInitSuccess ();
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
            //IAPOnInitFailed (error.ToString ());
        }


        //Called when a purchase succeeds.
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (String.Equals(args.purchasedProduct.definition.id, ProductID, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                IAPOnPurchaseSuccess();
            }

            return PurchaseProcessingResult.Complete;
        }


        //Called when a purchase fails.
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            IAPOnPurchaseFailed();
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }
    }


}

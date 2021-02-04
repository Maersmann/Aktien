/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:UI.Desktop"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Aktien.Logic.Messages.Aktie;
using Aktien.Logic.UI.AktieViewModels;
using Aktien.Logic.UI.DepotViewModels;
using Aktien.Logic.UI.DividendeViewModels;
using Aktien.Logic.UI.AktieViewModels.Page;
using Aktien.Logic.UI.AuswahlViewModels;
using Aktien.Logic.UI.ETFViewModels;
using Aktien.Logic.UI.WertpapierViewModels;

namespace Aktien.Logic.UI
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models                
            }
            else
            {
                // Create run time view services and models                
            }
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AktieStammdatenViewModel>();
            SimpleIoc.Default.Register<AktienUebersichtViewModel>();
            SimpleIoc.Default.Register<DividendeStammdatenViewModel>();
            SimpleIoc.Default.Register<DividendenUebersichtViewModel>();
            SimpleIoc.Default.Register<BuyOrderViewModel>();
            SimpleIoc.Default.Register<DepotUebersichtViewModel>();
            SimpleIoc.Default.Register<OrderUebersichtViewModel>();
            SimpleIoc.Default.Register<AktieUebersichtPageViewModel>();
            SimpleIoc.Default.Register<DividendeErhaltenViewModel>();
            SimpleIoc.Default.Register<DividendenUebersichtAuswahlViewModel>();
            SimpleIoc.Default.Register<DividendenAuswahlViewModel>();
            SimpleIoc.Default.Register<DividendeErhaltenUebersichtViewModel>();
            SimpleIoc.Default.Register<ETFStammdatenViewModel>();
            SimpleIoc.Default.Register<ETFGesamtUebersichtViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public AktieStammdatenViewModel AktieStammdaten => ServiceLocator.Current.GetInstance<AktieStammdatenViewModel>();
        public AktienUebersichtViewModel AktienUebersicht => ServiceLocator.Current.GetInstance<AktienUebersichtViewModel>();
        public DividendeStammdatenViewModel DividendeStammdaten => ServiceLocator.Current.GetInstance<DividendeStammdatenViewModel>();
        public DividendenUebersichtViewModel DividendenUebersicht => ServiceLocator.Current.GetInstance<DividendenUebersichtViewModel>();
        public BuyOrderViewModel BuyOrder => ServiceLocator.Current.GetInstance<BuyOrderViewModel>();
        public DepotUebersichtViewModel DepotUebersicht => ServiceLocator.Current.GetInstance<DepotUebersichtViewModel>();
        public OrderUebersichtViewModel OrderUebersicht => ServiceLocator.Current.GetInstance<OrderUebersichtViewModel>();
        public AktieUebersichtPageViewModel AktieUebersichtPage => ServiceLocator.Current.GetInstance<AktieUebersichtPageViewModel>();
        public DividendeErhaltenViewModel DividendeErhalten => ServiceLocator.Current.GetInstance<DividendeErhaltenViewModel>();
        public DividendenUebersichtAuswahlViewModel DividendenUebersichtAuswahl => ServiceLocator.Current.GetInstance<DividendenUebersichtAuswahlViewModel>();
        public DividendenAuswahlViewModel DividendenAuswahl => ServiceLocator.Current.GetInstance<DividendenAuswahlViewModel>();
        public DividendeErhaltenUebersichtViewModel DividendeErhaltenUebersicht => ServiceLocator.Current.GetInstance<DividendeErhaltenUebersichtViewModel>();
        public ETFStammdatenViewModel ETFStammdaten => ServiceLocator.Current.GetInstance<ETFStammdatenViewModel>();
        public ETFGesamtUebersichtViewModel ETFGesamtUebersicht => ServiceLocator.Current.GetInstance<ETFGesamtUebersichtViewModel>();
        public static void Cleanup()
        {

        }
    }
}
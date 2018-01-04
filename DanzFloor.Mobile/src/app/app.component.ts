import { Component, ViewChild } from '@angular/core';

import { Platform, MenuController, App, Nav, ToastController } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { isCordovaAvailable } from '../services/is-cordova-available';
import { GoogleAnalytics } from '@ionic-native/google-analytics';
import { AppVersion } from '@ionic-native/app-version';

//import { OAuthService } from '../pages/oauth/oauth.service';
import { HomePage } from '../pages/home/home.page';
// import { WordpressListPage } from '../pages/wordpress/list/wordpress.list.page';
// import { DrupalListPage } from '../pages/drupal/list/drupal.list.page';
// import { SlideBoxPage } from '../pages/slide-box/slide-box.page';
// import { YoutubeListPage } from '../pages/youtube/list/youtube.list.page';
// import { VimeoListPage } from '../pages/vimeo/list/vimeo.list.page';
// import { NewsListPage } from '../pages/news/list/news.list.page';
// import { ProductsListPage } from '../pages/products/list/products.list.page';
// import { LocalStorageListPage } from '../pages/local-storage/list/local-storage.list.page';
// import { SqliteListPage } from '../pages/sqlite/list/sqlite.list.page';
// import { OAuthProvidersListPage } from '../pages/oauth/list/oauth-providers.list.page';
// import { OAuthProfilePage } from '../pages/oauth/profile/oauth-profile.page';
// import { DateTimePage } from '../pages/date-time/date-time.page';
// import { NetworkPage } from '../pages/network/network.page';
// import { BarcodeScannerPage } from '../pages/barcode-scanner/barcode-scanner.page';
// import { ToastsPage } from '../pages/toasts/toasts.page';
// import { VibratePage } from '../pages/vibrate/vibrate.page';
// import { SpinnerDialogPage } from '../pages/spinner-dialog/spinner-dialog.page';
// import { FlashlightPage } from '../pages/flashlight/flashlight.page';
// import { SocialSharingPage } from '../pages/social-sharing/social-sharing.page';
// import { ActionsheetPage } from '../pages/actionsheet/actionsheet.page';
// import { SimPage } from '../pages/sim/sim.page';
// import { BrightnessPage } from '../pages/brightness/brightness.page';
// import { CallNumberPage } from '../pages/call-number/call-number.page';
// import { CameraPage } from '../pages/camera/camera.page';
// import { CropPage } from '../pages/crop/crop.page';
// import { ScreenOrientationPage } from '../pages/screen-orientation/screen-orientation.page';
// import { PhotoViewerPage } from '../pages/photo-viewer/photo-viewer.page';
// import { LocalNotificationsPage } from '../pages/local-notifications/local-notifications.page';
// import { DeviceMotionPage } from '../pages/device-motion/device-motion.page';
// import { CalendarPage } from '../pages/calendar/calendar.page';
// import { DialogsPage } from '../pages/dialogs/dialogs.page';
// import { NativeStorageListPage } from '../pages/native-storage/list/native-storage.list.page';
// import { BadgePage } from '../pages/badge/badge.page';
// import { ClipboardPage } from '../pages/clipboard/clipboard.page';
// import { ContactsPage } from '../pages/contacts/contacts.page';
// import { DevicePage } from '../pages/device/device.page';
// import { AppAvailabilityPage } from '../pages/app-availability/app-availability.page';
// import { ShakePage } from '../pages/shake/shake.page';
// import { DatePickerPage } from '../pages/date-picker/date-picker.page';
// import { YoutubeVideoPlayerPage } from '../pages/youtube-video-player/youtube-video-player.page';
// import { TextToSpeechPage } from '../pages/text-to-speech/text-to-speech.page';
// import { PushPage } from '../pages/push/push.page';
// import { ComponentsListPage } from '../pages/components/list/components.list.page';
// import { LaunchNavigatorPage } from '../pages/launch-navigator/launch-navigator.page';
// import { RSSFeedsPage } from '../pages/rss-feeds/list/rss-feeds.page';
// import { PositionPage } from '../pages/position/position.page';
// import { GalleriesPage } from '../pages/galleries/list/galleries.page';
// import { GoogleMapsPage } from '../pages/google-maps/google-maps.page';
// import { StripePage } from '../pages/stripe/stripe.page';
// import { PayPalPage } from '../pages/paypal/paypal.page';
// import { StreamingMediaPage } from '../pages/streaming-media/streaming-media.page';
// import { ThemeableBrowserPage } from '../pages/themeable-browser/themeable-browser.page';
// import { InsomniaPage } from '../pages/insomnia/insomnia.page';
// import { CustomFlashCardPage } from '../pages/custom-flash-card/custom-flash-card.page';
// import { FabToolbarPage } from '../pages/fab-toolbar/fab-toolbar.page';
// import { ChartsListPage } from '../pages/charts-page/list/charts.list.page';
// import { TinderCardsPage } from '../pages/tinder-cards/tinder-cards.page';
// import { I18nPage } from '../pages/i18n-capabilities/i18n.page';
// import { LayoutsListPage } from '../pages/layouts/list/layouts.list.page';
// import { InstagramLoginPage } from '../pages/instagram/login/instagram-login.page';
// import { AdMobPage } from '../pages/adMob/adMob.page';
// import { SlackPage } from '../pages/slack/slack.page';
// import { GooglePlaceAutocompletePage } from '../pages/google-place-autocomplete/google-place-autocomplete.page';
// import { BadgedTabsPage } from '../pages/badged-tabs/badged-tabs.page';
// import { GoogleAnalyticsPage } from '../pages/google-analytics/google-analytics.page';
// import { FirebaseCrudListPage } from '../pages/firebase/list/firebase-crud.list.page';
// import { PouchDbCrudListPage } from '../pages/pouchdb-crud/list/pouchdb-crud.list.page';
import { SplashScreen } from '@ionic-native/splash-screen';

import { LoginComponent } from "../pages/login/login.component";
import { Network } from '@ionic-native/network';
import { SharedService } from '../services/shared.service';
import { OneSignal } from '@ionic-native/onesignal';
import { ScreenOrientation } from '@ionic-native/screen-orientation';

@Component({
	templateUrl: 'app.html',
	providers: [SplashScreen, SharedService,GoogleAnalytics,AppVersion]
})
export class MyApp {
	wide: boolean = false;
	genericPages;
	nativePages;
	databasePages;
	thirdpartyPages;
	paymentsPages;
	cssComponentsPages;
	homePage;
	rootPage;

	private app;
	private platform;
	private menu: MenuController;
	networkStatus: string;
	//private oauthService: OAuthService;

	@ViewChild(Nav) nav: Nav;

	constructor(
		platform: Platform,
		menu: MenuController,
		private splashScreen: SplashScreen,
		private sharedService: SharedService,
		
		/* oauthService: OAuthService,*/
		app: App,
		private statusBar: StatusBar,
		private network: Network,
		private toast: ToastController,
		private oneSignal: OneSignal,
		private ga: GoogleAnalytics,
        private appVersion: AppVersion,
		private screenOrientation:ScreenOrientation
	) {

		this.menu = menu;
		//this.oauthService = oauthService;
		// set up our app
		this.app = app;
		this.platform = platform;
		this.initializeApp();

		// set our app's pages
		this.homePage = { title: 'Home', component: HomePage, icon: 'home' };

		this.genericPages = [
			{ title: 'Login', component: LoginComponent, icon: 'pricetag' },
			// { title: 'DateTime', component: DateTimePage, icon: 'time' },
			//{ title: 'Drupal', component: DrupalListPage, icon: 'water' },
			//{ title: 'Galleries', component: GalleriesPage, icon: 'images' },
			// { title: 'Google maps', component: GoogleMapsPage, icon: 'map' },
			// { title: 'News', component: NewsListPage, icon: 'paper' },
			// { title: 'OAuth', component: OAuthProvidersListPage, icon: 'log-in' },
			// { title: 'Products', component: ProductsListPage, icon: 'archive' },
			// { title: 'RSS Feeds', component: RSSFeedsPage, icon: 'logo-rss' },
			// { title: 'Slack', component: SlackPage, icon: 'send' },
			// { title: 'Slides', component: SlideBoxPage, icon: 'swap' },
			// { title: 'Vimeo', component: VimeoListPage, icon: 'logo-vimeo' },
			// { title: 'Wordpress', component: WordpressListPage, icon: 'logo-wordpress' },
			// { title: 'YouTube', component: YoutubeListPage, icon: 'logo-youtube' }
		];

		this.nativePages = [
			// { title: 'Action sheet', component: ActionsheetPage, icon: 'list-box' },
			// { title: 'App availability', component: AppAvailabilityPage, icon: 'apps' },
			// { title: 'Badge', component: BadgePage, icon: 'ionic' },
			// { title: 'BarcodeScanner', component: BarcodeScannerPage, icon: 'qr-scanner' },
			// { title: 'Brightness', component: BrightnessPage, icon: 'sunny' },
			// { title: 'Calendar', component: CalendarPage, icon: 'calendar' },
			// { title: 'Call number', component: CallNumberPage, icon: 'call' },
			// { title: 'Camera', component: CameraPage, icon: 'camera' },
			// { title: 'Clipboard', component: ClipboardPage, icon: 'clipboard' },
			// { title: 'Contacts', component: ContactsPage, icon: 'contacts' },
			// { title: 'Crop', component: CropPage, icon: 'crop' },
			// { title: 'Date picker', component: DatePickerPage, icon: 'calendar' },
			// { title: 'Device', component: DevicePage, icon: 'logo-android' },
			// { title: 'Device motion', component: DeviceMotionPage, icon: 'contract' },
			// { title: 'Dialogs', component: DialogsPage, icon: 'alert' },
			// { title: 'Flashlight', component: FlashlightPage, icon: 'flash' },
			// { title: 'Google analytics', component: GoogleAnalyticsPage, icon: 'logo-google' },
			// { title: 'Launch navigator', component: LaunchNavigatorPage, icon: 'pin' },
			// { title: 'Local notifications', component: LocalNotificationsPage, icon: 'notifications' },
			// { title: 'Network', component: NetworkPage, icon: 'wifi' },
			// { title: 'Photo viewer', component: PhotoViewerPage, icon: 'images' },
			// { title: 'Position', component: PositionPage, icon: 'compass' },
			// { title: 'Push', component: PushPage, icon: 'notifications' },
			// { title: 'Screen orientation', component: ScreenOrientationPage, icon: 'expand' },
			// { title: 'Shake', component: ShakePage, icon: 'hand' },
			// { title: 'Sim', component: SimPage, icon: 'information-circle' },
			// { title: 'SocialSharing', component: SocialSharingPage, icon: 'share' },
			// { title: 'SpinnerDialog', component: SpinnerDialogPage, icon: 'sync' },
			// { title: 'Text to speech', component: TextToSpeechPage, icon: 'microphone' },
			// { title: 'Toasts', component: ToastsPage, icon: 'information-circle' },
			// { title: 'Vibrate', component: VibratePage, icon: 'phone-portrait' },
			// { title: 'Youtube player', component: YoutubeVideoPlayerPage, icon: 'logo-youtube' },
			// { title: 'Streaming media', component: StreamingMediaPage, icon: 'logo-playstation' },
			// { title: 'Themeable browser', component: ThemeableBrowserPage, icon: 'color-palette' },
			// { title: 'Insomnia', component: InsomniaPage, icon: 'moon' },
			// { title: 'Instagram', component: InstagramLoginPage, icon: 'logo-instagram' }
		];

		this.databasePages = [
			// { title: 'Firebase', component: FirebaseCrudListPage, icon: 'flame' },
			// { title: 'Local storage', component: LocalStorageListPage, icon: 'browsers' },
			// { title: 'Native storage', component: NativeStorageListPage, icon: 'logo-buffer' },
			// { title: 'Pouch/Couch DB', component: PouchDbCrudListPage, icon: 'cloud' },
			// { title: 'SQLite', component: SqliteListPage, icon: 'cube' }
		];

		this.thirdpartyPages = [
			// { title: 'Charts', component: ChartsListPage, icon: 'stats' },
			// { title: 'Fab toolbar', component: FabToolbarPage, icon: 'list-box' },
			// { title: 'Custom flash card', component: CustomFlashCardPage, icon: 'refresh' },
			// { title: 'Tinder cards', component: TinderCardsPage, icon: 'swap' },
			// { title: 'i18n', component: I18nPage, icon: 'globe' },
			// { title: 'Google places', component: GooglePlaceAutocompletePage, icon: 'locate' },

		];

		this.paymentsPages = [
			// { title: 'Stripe', component: StripePage, icon: 'card' },
			// { title: 'PayPal', component: PayPalPage, icon: 'cash' },
			// { title: 'AdMob', component: AdMobPage, icon: 'cloud' }
		];

		this.cssComponentsPages = [
			// { title: 'Components', component: ComponentsListPage, icon: 'grid' },
			// { title: 'Layouts', component: LayoutsListPage, icon: 'flower' }
		];

		this.rootPage = LoginComponent;
	}

	public Toast(message: string) {
		this.toast.create({
			message: message,
			duration: 3000
		}).present();
	}

	initializeApp() {
		this.platform.ready().then(() => {
			//(window as any).FirebaseCrashReport.log("Test 30-6 !");
			//Segun el template Analytics tendria que estar inicializado en el module.
			this.screenOrientation.lock(this.screenOrientation.ORIENTATIONS.PORTRAIT);
			this.ga.startTrackerWithId('UA-101584013-1')
				.then(() => {
					console.log('Google analytics is ready now');
					//the component is ready and you can call any method here
					this.ga.debugMode();
					this.appVersion.getVersionNumber().then(v=>this.ga.setAppVersion(v));
					
					this.ga.setAllowIDFACollection(true);
					//this.ga.enableUncaughtExceptionReporting(true);
					this.ga.trackEvent("App","opened");
				})
				.catch(e => console.log('Error starting GoogleAnalytics', e)); 
			this.statusBar.styleDefault();
			this.splashScreen.hide();

			console.log(this.network.type);
			if (this.network.type != 'none')
				this.sharedService.networkConnected = true;

			// watch network for a disconnect
			this.network.onDisconnect().subscribe(data => {
				//console.log(data);
				if (this.sharedService.networkConnected) {
					this.sharedService.networkConnected = false;
					this.Toast(`Sin acceso a internet :(`);
				}
			});

			// stop disconnect watch
			//disconnectSubscription.unsubscribe();


			// watch network for a connection
			this.network.onConnect().subscribe(data => {
				//console.log(data);
				//console.log('network connected!');
				if (!this.sharedService.networkConnected) {
					this.sharedService.networkConnected = true;
					this.Toast(`Conectado a internet! :)`);
				}
				// We just got a connection but we need to wait briefly
				// before we determine the connection type. Might need to wait.
				// prior to doing any api requests as well.
				/*setTimeout(() => {
					if (this.network.type === 'wifi') {
						console.log('we got a wifi connection, woohoo!');
					}
					
				}, 3000);*/
			});

			if (isCordovaAvailable()) {
			 	this.oneSignal.startInit('ccbedeff-9936-4f74-a8b7-46dd91f206f2', '888160053125');

			 	this.oneSignal.inFocusDisplaying(this.oneSignal.OSInFocusDisplayOption.Notification);

			 	this.oneSignal.handleNotificationReceived().subscribe(() => {
			 		// do something when notification is received
				});

			 	this.oneSignal.handleNotificationOpened().subscribe(() => {
			 		// do something when a notification is opened
			 	});

				this.oneSignal.getIds().then(one=>this.sharedService.setOneSignal(one));

			 	this.oneSignal.endInit();

			 	// stop connect watch
			 	//connectSubscription.unsubscribe();
			 }
		});
	}

	openPage(page) {
		// close the menu when clicking a link from the menu
		this.menu.close();
		// navigate to the new page if it is not the current page
		let component = page.component;
		// if (component === OAuthProvidersListPage && this.oauthService.isAuthorized()) {
		// 	component = OAuthProfilePage;
		// }

		this.nav.setRoot(component);
	}

	setWidth() {
		if (this.platform.width() > 767) {
			this.wide = true;
			this.menu.open();
		} else {
			this.wide = false;
			this.menu.close();
		}
	};

	listenToEvents() {
		window.addEventListener('resize', () => {
			this.setWidth();
		});
	}
}

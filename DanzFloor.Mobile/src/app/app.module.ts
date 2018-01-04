//import { AgmCoreModule } from '@agm/core';
import { ErrorHandler, NgModule } from '@angular/core';
import { Http, HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { StatusBar } from '@ionic-native/status-bar';
// import { CloudModule } from '@ionic/cloud-angular';
import { IonicStorageModule } from '@ionic/storage';
// import { AngularFireModule } from 'angularfire2';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
// import { TranslateModule } from 'ng2-translate/ng2-translate';
// import { TranslateLoader, TranslateStaticLoader } from 'ng2-translate/src/translate.service';
// import { CustomComponentsModule } from '../components/custom-components.module';
import { Config } from '../config';
// import { ActionsheetModule } from '../pages/actionsheet/actionsheet.module';
// import { AdMobModule } from '../pages/adMob/adMob.module';
// import { AppAvailabilityModule } from '../pages/app-availability/app-availability.module';
// import { AppRateModule } from '../pages/app-rate/app-rate.module';
// import { BadgeModule } from '../pages/badge/badge.module';
// import { BadgedTabsModule } from '../pages/badged-tabs/badged-tabs.module';
// import { BarcodeScannerModule } from '../pages/barcode-scanner/barcode-scanner.module';
// import { BrightnessModule } from '../pages/brightness/brightness.module';
// import { CalendarModule } from '../pages/calendar/calendar.module';
// import { CallNumberModule } from '../pages/call-number/call-number.module';
// import { CameraModule } from '../pages/camera/camera.module';
// import { ChartsPageModule } from '../pages/charts-page/charts-page.module';
// import { ClipboardModule } from '../pages/clipboard/clipboard.module';
// import { ComponentsModule } from '../pages/components/components.module';
// import { ContactsModule } from '../pages/contacts/contacts.module';
// import { CropModule } from '../pages/crop/crop.module';
// import { CustomFlashCardModule } from '../pages/custom-flash-card/custom-flash-card.module';
// import { DatePickerModule } from '../pages/date-picker/date-picker.module';
// import { DateTimeModule } from '../pages/date-time/date-time.module';
// import { DeviceMotionModule } from '../pages/device-motion/device-motion.module';
// import { DeviceModule } from '../pages/device/device.module';
// import { DialogsModule } from '../pages/dialogs/dialogs.module';
// import { DrupalModule } from '../pages/drupal/drupal.module';
// import { FabToolbarModule } from '../pages/fab-toolbar/fab-toolbar.module';
// import { FirebaseCrudModule } from '../pages/firebase/firebase-crud.module';
// import { FlashlightModule } from '../pages/flashlight/flashlight.module';
// import { GalleriesModule } from '../pages/galleries/galleries.module';
// import { GoogleAnalyticsModule } from '../pages/google-analytics/google-analytics.module';
// import { GoogleMapsModule } from '../pages/google-maps/google-maps.module';
// import { GooglePlaceAutocompleteModule } from '../pages/google-place-autocomplete/google-place-autocomplete.module';
import { HomeModule } from '../pages/home/home.module';
// import { I18nModule } from '../pages/i18n-capabilities/i18n-capabilities.module';
// import { InsomniaModule } from '../pages/insomnia/insomnia.module';
// import { InstagramModule } from '../pages/instagram/instagram.module';
// import { LaunchNavigatorModule } from '../pages/launch-navigator/launch-navigator.module';
// import { LayoutsModule } from '../pages/layouts/layouts.module';
// import { LocalNotificationsModule } from '../pages/local-notifications/local-notifications.module';
// import { LocalStorageModule } from '../pages/local-storage/local-storage.module';
// import { NativeStorageModule } from '../pages/native-storage/native-storage.module';
// import { NetworkModule } from '../pages/network/network.module';
// import { NewsModule } from '../pages/news/news.module';
// import { OAuthModule } from '../pages/oauth/oauth.module';
// import { PayPalModule } from '../pages/paypal/paypal.module';
// import { PhotoViewerModule } from '../pages/photo-viewer/photo-viewer.module';
// import { PositionModule } from '../pages/position/position.module';
// import { PouchDbCrudModule } from '../pages/pouchdb-crud/pouchdb-crud.module';
// import { ProductsModule } from '../pages/products/products.module';
// import { PushModule } from '../pages/push/push.module';
// import { RssFeedsModule } from '../pages/rss-feeds/rss-feeds.module';
// import { ScreenOrientationModule } from '../pages/screen-orientation/screen-orientation.module';
// import { ShakeModule } from '../pages/shake/shake.module';
// import { SimModule } from '../pages/sim/sim.module';
// import { SlackModule } from '../pages/slack/slack.module';
// import { SlideBoxModule } from '../pages/slide-box/slide-box.module';
// import { SocialSharingModule } from '../pages/social-sharing/social-sharing.module';
// import { SpinnerDialogModule } from '../pages/spinner-dialog/spinner-dialog.module';
// import { SqliteModule } from '../pages/sqlite/sqlite.module';
// import { StreamingMediaModule } from '../pages/streaming-media/streaming-media.module';
// import { StripeModule } from '../pages/stripe/stripe.module';
// import { TextToSpeechModule } from '../pages/text-to-speech/text-to-speech.module';
// import { ThemeableBrowserModule } from '../pages/themeable-browser/themeable-browser.module';
// import { TinderCardsModule } from '../pages/tinder-cards/tinder-cards.module';
// import { ToastsModule } from '../pages/toasts/toasts.module';
// import { VibrateModule } from '../pages/vibrate/vibrate.module';
// import { VimeoModule } from '../pages/vimeo/vimeo.module';
// import { WordpressModule } from '../pages/wordpress/wordpress.module';
// import { YoutubeVideoPlayerModule } from '../pages/youtube-video-player/youtube-video-player.module';
// import { YoutubeModule } from '../pages/youtube/youtube.module';
// import { Base64Service } from '../services/base64.service';
import { MyApp } from './app.component';

import { LoginModule } from "../pages/login/login.module";

import { Network } from '@ionic-native/network';
import { OneSignal } from '@ionic-native/onesignal';
import { ScreenOrientation } from '@ionic-native/screen-orientation';

import { CloudSettings, CloudModule } from '@ionic/cloud-angular';

export function createTranslateLoader(http: Http) {
	//return new TranslateStaticLoader(http, 'assets/i18n', '.json');
}

const cloudSettings: CloudSettings = {
	'core': {
	  'app_id': '58cf8780'
	}
  };

@NgModule({
	declarations: [
		MyApp
	],
	imports: [
		BrowserModule,
		HttpModule,
		IonicStorageModule.forRoot(),
		IonicModule.forRoot(MyApp,{
			mode: 'md'
		}),

		LoginModule,

		CloudModule.forRoot(cloudSettings),
		// AgmCoreModule.forRoot(),
		// TranslateModule.forRoot({
		// 	provide: TranslateLoader,
		// 	useFactory: (createTranslateLoader),
		// 	deps: [Http]
		// }),
		// AngularFireModule.initializeApp(Config.firebase),

		// CustomComponentsModule,

		// ActionsheetModule,
		// AppAvailabilityModule,
		// ComponentsModule,
		// BadgeModule,
		// BadgedTabsModule,
		// GoogleAnalyticsModule,
		// NewsModule,
		// BarcodeScannerModule,
		// BrightnessModule,
		// CalendarModule,
		// CallNumberModule,
		// CameraModule,
		// ClipboardModule,
		// ContactsModule,
		// CropModule,
		// DatePickerModule,
		// DateTimeModule,
		// DeviceModule,
		// DeviceMotionModule,
		// DialogsModule,
		// DrupalModule,
		// FlashlightModule,
		HomeModule,
		// LocalNotificationsModule,
		// NativeStorageModule,
		// LocalStorageModule,
		// NetworkModule,
		// OAuthModule,
		// PhotoViewerModule,
		// PositionModule,
		// ProductsModule,
		// PushModule,
		// ScreenOrientationModule,
		// ShakeModule,
		// SimModule,
		// SlideBoxModule,
		// SocialSharingModule,
		// SpinnerDialogModule,
		// SqliteModule,
		// TextToSpeechModule,
		// ToastsModule,
		// VibrateModule,
		// VimeoModule,
		// WordpressModule,
		// YoutubeModule,
		// YoutubeVideoPlayerModule,
		// RssFeedsModule,
		// GalleriesModule,
		// GoogleMapsModule,
		// GooglePlaceAutocompleteModule,
		// LaunchNavigatorModule,
		// StripeModule,
		// PayPalModule,
		// AppRateModule,
		// StreamingMediaModule,
		// ThemeableBrowserModule,
		// InsomniaModule,
		// CustomFlashCardModule,
		// FabToolbarModule,
		// ChartsPageModule,
		// TinderCardsModule,
		// I18nModule,
		// LayoutsModule,
		// InstagramModule,
		// AdMobModule,
		// SlackModule,
		// FirebaseCrudModule,
		// PouchDbCrudModule
	],
	bootstrap: [IonicApp],
	entryComponents: [
		MyApp
	],
	providers: [
		Config,
		Network,
		OneSignal,
		// Base64Service,
		{ provide: ErrorHandler, useClass: IonicErrorHandler },
		ScreenOrientation,
		StatusBar
	]
})
export class AppModule {
}

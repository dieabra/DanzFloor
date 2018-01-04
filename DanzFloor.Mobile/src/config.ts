import { Injectable } from '@angular/core';
/*import { CloudSettings } from '@ionic/cloud-angular';*/

@Injectable()
export class Config {
	public wordpressApiUrl = 'http://demo.titaniumtemplates.com/wordpress/?json=1';
	public drupalApiUrl = 'http://demo.titaniumtemplates.com/drupal/rest/views/rest_api';
	public youtube = {
		apiUrl: 'https://www.googleapis.com/youtube/v3/',
		key: 'AIzaSyDael5MmCQa1GKQNKQYypmBeB08GATgSEo',
		itemsPerPage: 20,
		username: 'google'
	};
	public vimeo = {
		userId: 'user13092665',
		accessToken: 'd995ffff0228beba7c9716c3ee0d4add',
		apiUrl: 'https://api.vimeo.com/users/',
		itemsPerPage: 20
	};
	public newsUrl = 'http://skounis.s3.amazonaws.com/mobile-apps/barebone-glossy/news.json';
	public productsUrl = 'http://skounis.s3.amazonaws.com/mobile-apps/barebone-glossy/products.json';
	public facebook = {
		apiUrl: 'https://graph.facebook.com/v2.3/',
		appId: '927897987270774',
		scope: ['email']
	};
	public google = {
		apiUrl: 'https://www.googleapis.com/oauth2/v3/',
		appId: '400671186930-m07eu77bm43tgr30p90k6b9e1qgsva4p.apps.googleusercontent.com',
		scope: ['email']
	};
	public github = {
		apiUrl: 'https://api.github.com/',
		appId: 'ad0b24caa066f59d4971',
		appSecret: '2f1ecf23ebb9ba28332203a38115c90ed2e0a3fe',
		scope: ['user']
	};
	public twitter = {
		apiKey: 'wXRPbDKzyLXOy4etLq4fNqu8M',
		apiSecret: '1Bi6DGM98yc9MToSLstGLFaB2tvHOLkBrBBYm8WWI2fTKl0gWX'
	};
	public linkedin = {
		apiUrl: 'https://api.linkedin.com/v1/',
		accessTokenUrl: 'https://www.linkedin.com/oauth/v2/accessToken',
		appId: '86ysxssoycble9',
		appSecret: 'sy9uHc0EIqG4fe6i',
		scope: ['r_basicprofile', 'r_emailaddress'],
		redirectUrl: 'http://localhost/callback'
	};
	public ionicSecurityProfile = 'test';
	public ionicCloudApiToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI2NGFjN2ZjMS1iNzAyLTRmOWMtOTFmOS0zZGE0YjA3MGJkNzcifQ.XiQkjfLm9U3Irnab6uQqgSXZ9Ilrt1LQfUTETJfLvbA';
	static videoUrl = 'http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4';
	static audioUrl = 'http://www.stephaniequinn.com/Music/Allegro%20from%20Duet%20in%20C%20Major.mp3';
	static sender_id = '211377410430';
	static app_id = 'f7f7ef47';
	// Paypal
	static payPalEnvironmentSandbox = 'AUp3xO-yveZDMTjZ20GWJO6c_tv7bbHrj3sZC__XyaQ7N64iVd49HRyi5WBPD00ojcHK41_hvl76PbzH';
	static payPalEnvironmentProduction = '';
	// Apprate - Application URLs
	static iosUrl = 'com.titaniumtemplates.barebone-ionic';
	static androidUrl = 'market://details?id=com.titaniumtemplates.barebone-ionic';
	static usesUntilRatePrompt = 3;
	// Instagram APP ID
	static instagramAppId = 'ab4ccebff87a46e789e231bed83685e4';
	// AdMob Publisher Keys
	static androidPublisherKey = 'ca-app-pub-3965039466794589/2790557649';
	static iosPublisherKey = 'ca-app-pub-3965039466794589/2930158449';
	static slackIncomingWebHookUrl = 'https://hooks.slack.com/services/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';
	// Google Analytics
	static googleAnalyticsTrackedID = 'UA-42570451-7';
	static googleAnalyticsAppVersion = 'v1.15';
	static googleAnalyticsUserID = 'appseed';

	static firebase = {
		apiKey: 'AIzaSyArKvh_hPjlRG-w8etOasNjY4C3gE7wjuw',
		authDomain: 'barebone-ionic-25a9c.firebaseapp.com',
		databaseURL: 'https://barebone-ionic-25a9c.firebaseio.com',
		storageBucket: 'barebone-ionic-25a9c.appspot.com',
		messagingSenderId: '417880833124'
	};

	// CouchDB/PouchDB/Cloudand
	// Enable CORS - https://cl.ly/3U2s0r3u3e0J
	// key/password - generate here https://cl.ly/2y0v0Y3P1E1k
	static couchdb = {
		key: 'aysionshertempaceldshene',
		password: '17ffee7ffa1807a231ee564b51d2244ea85c7daf',
		local: 'bare2',
		remote: 'https://ionic.cloudant.com/bare2'
	}
}

/*export const cloudSettings: CloudSettings = {
	'core': {
		'app_id': Config.app_id,
	},
	'push': {
		'sender_id': Config.sender_id
	}
};*/

import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

import { NavParams } from 'ionic-angular';
import { VimeoService } from '../../../services/vimeo.service';
import { EpisodioService } from '../../../services/episodio.service';
import { EntornoConfig } from "../../../constants/entorno.config";
import { CanalItem } from '../canalItem/canalItem.component';
import { SharedService } from "../../../services/shared.service";
import { SocialSharing } from '@ionic-native/social-sharing';
import { MediaPlayerService } from '../../../services/media-player.service'
import { GoogleAnalytics } from '@ionic-native/google-analytics';


@Component({
	templateUrl: 'videoItem.component.html',
	providers: [VimeoService, EpisodioService, EntornoConfig, SharedService,MediaPlayerService ]
})
export class VideoItem {
	private sharedService: SharedService;
	video: any;
	videos: any[] = [];
	episodios: any[];
	PageIndex = 1;
	PageSize = 3;
	Pages = null;
	playerid: string = "";
	backbutton :string = "";
	private nav: NavController;
	private currentVideoId : string;

	constructor(
		private episodioService: EpisodioService,
	 	private service: VimeoService,
		private _sharedService: SharedService,
	 	navParams: NavParams, 
		nav: NavController,
		private socialSharing: SocialSharing,
		private entornoConfig: EntornoConfig, 
		private mediaService: MediaPlayerService,
		private ga: GoogleAnalytics) {
		this.video = navParams.get('item');
		this.nav = nav;
	}

	ngOnInit(): void {
		let ultimoReproductor = this._sharedService.ObtenerUltimoReproductorActivo();
		this.mediaService.stopMedia(ultimoReproductor);
		this.playerid = this._sharedService.ObtenerNuevoIdParaJWPlayer();	
		this.backbutton =  this.playerid.replace('MyMediaDiv','videoBackButton');	
		this.ga.trackView('View: Reproducir');
		
		  this.episodioService.PorPrograma(this.video.Programa.Id, this.PageIndex, this.PageSize, this.video.Id)
		  	.subscribe(respuesta => {
				  this.episodios = respuesta.Result;
				  this.video = respuesta.video;
				  this.currentVideoId = respuesta.video.Id;
				this.mediaService.loadMedia({
				Title: this.video.Nombre,
				Location: this.video.Video,
				Identificador: this.video.Identificador,
				Id: this.video.Id,
				PlayerId: this.playerid,
				BackButton: this.backbutton
				//Location:'https://videos.DanzFloor.com/' + this.video.Video 
			},true, this.playerid).then(p=>console.log('jwPlayer cargo bien :)'),e=> console.log('jwPlayer cargo mal :(. \n'+e));
		 	},
			  error => { console.log(error) });
			this.ga.trackEvent('Reproducir', 'Video' + this.video.Nombre );
		// this.episodioService.GetEpisodio(this.video.Id)
		//  	.subscribe(respuesta => {
		// 		 this.video = respuesta.Result;
		// 				  this.mediaService.loadMedia({
		// 		Title: this.video.Nombre,
		// 		Location: this.video.Video 
		// 		//Location:'https://videos.DanzFloor.com/' + this.video.Video 
		// 	},true).then(p=>console.log('jwPlayer cargo bien :)'),e=> console.log('jwPlayer cargo mal :(. \n'+e));
		//  	},
		//  	error => { console.log(error) });
			
	}

	goBack(){
		console.log('back');
		 this.nav.pop();
	}

	ObtenerImagen(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-256|width-453|mode-crop|quality-100|';
	}
	
	ObtenerImagenPortadaEpisodio(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-160|width-111|mode-crop|quality-100|';
	}

	ObtenerImagenPrograma(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-40|width-40|mode-crop|quality-100|';
	}
	
	itemTappedPrograma(item) {	
		 let ultimoReproductor = this._sharedService.ObtenerUltimoReproductorActivo();
		 this.mediaService.stopMedia(ultimoReproductor);
		 this.nav.push(CanalItem, {
		 	item: item
		 });
	}

	itemTapped(item) {
		this.ga.trackEvent('Reproducir', 'VideoRelacionado');		
		let ultimoReproductor = this._sharedService.ObtenerUltimoReproductorActivo();
		this.mediaService.stopMedia(ultimoReproductor);
		this.video = item;
		this.currentVideoId = item.Id;
		this.mediaService.loadMedia({
			Title: this.video.Nombre,
			Location: this.video.Video,
				Identificador: this.video.Identificador,
				Id: this.video.Id,
				Thumbnail: this.video.Thumbnail,
				PlayerId: this.playerid,
				BackButton: this.backbutton
			//Location:'https://videos.DanzFloor.com/' + this.video.Video 
		},true,  this.playerid).then(p=>console.log('jwPlayer cargo bien :)'),e=> console.log('jwPlayer cargo mal :(. \n'+e));
		this.ga.trackEvent('Reproducir', 'Video' + this.video.Nombre );
	}

	like(item) {
		this.ga.trackEvent('Novedades', 'Like');
		item.Like = !item.Like;

		if (item.Like)
			item.CantidadLikes = item.CantidadLikes + 1;
		else
			item.CantidadLikes = item.CantidadLikes - 1;

		this.episodioService.Like(item.Id, item.Like)
			.subscribe(respuesta => {

			},
			error => { console.log(error) });
	}

	public share(episodio: any) {
		this.ga.trackEvent('Novedades', 'Share');
		this.socialSharing.shareWithOptions({ url: this._sharedService.Core + '/FrontEnd/Reproducir/' + episodio.Id })
	}

	bookmark(item) {
		this.ga.trackEvent('Novedades', 'Bookmark');
		item.Bookmark = !item.Bookmark;

		this.episodioService.Bookmark(item.Id, item.Bookmark)
			.subscribe(respuesta => {

			},
			error => { console.log(error) });
	}

	doRefresh(refresher) {
		this.ga.trackEvent('Reproducir', 'Refresh');
		this.PageIndex = 1;		
		this.episodioService.PorPrograma(this.video.Programa.Id,this.PageIndex, this.PageSize, this.video.Id)
			.subscribe(respuesta => {
				this.episodios = respuesta.Result;
				if (respuesta.video != null){
				this.video = respuesta.video;}
				refresher.complete();
			},
			error => { 
				console.log(error);
				refresher.complete();
			});
	}

	doInfinite(): Promise<any> {

		return new Promise((resolve) => {

			this.PageIndex = this.PageIndex + 1;
			
			if(this.Pages != null && this.PageIndex > this.Pages)
				{
					resolve();
					return;
				}
            
			this.episodioService.PorPrograma(this.video.Programa.Id, this.PageIndex, this.PageSize, this.video.Id)
			.subscribe(respuesta => {
					this.ga.trackEvent('Reproducir', 'TraerMasEpisodios');
					for (var i = 0; i < respuesta.Result.length; i++) {
						this.episodios.push(respuesta.Result[i]);
					}
					this.Pages = respuesta.Pages;
					console.log('Finaliza la carga ' + this.PageIndex);
				resolve();
			},
				error => { 
					console.log(error);
					resolve();
			});

		})
	}
}


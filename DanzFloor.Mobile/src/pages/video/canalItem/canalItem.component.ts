import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

import { NavParams } from 'ionic-angular';

import { SharedService} from "../../../services/shared.service";

import { ProgramaService } from '../../../services/programa.service';
import { EpisodioService } from '../../../services/episodio.service';
import { EntornoConfig } from "../../../constants/entorno.config";
import { GoogleAnalytics } from '@ionic-native/google-analytics';
import { SocialSharing } from '@ionic-native/social-sharing';
import { VideoItem } from '../videoItem/videoItem.component'

import { OneSignal } from '@ionic-native/onesignal';

@Component({
	templateUrl: 'canalItem.component.html',
	providers: [EntornoConfig, EpisodioService, ProgramaService]
})
export class CanalItem {
	private nav: NavController;
	    private sharedService: SharedService;

	programa: any;
	videos: any[] = [];
	
	private PageIndex: any = 1;
	private PageSize: any = 3;
	private Pages: any = null;

	constructor(
		navParams: NavParams,
		nav: NavController,
		private entornoConfig: EntornoConfig,
		private socialSharing: SocialSharing,
		private ga: GoogleAnalytics,
		private episodioService: EpisodioService,
		private _sharedService: SharedService,
		private oneSignal:OneSignal,
		private programaService: ProgramaService) {
		this.programa = navParams.get('item');		
		this.nav = nav;
	}

	ngOnInit(): void {
		this.ga.trackView('View: ProgramaPerfil');

		this.programaService.porId(this.programa.Id)
			.subscribe(respuesta => {
				this.programa = respuesta.Result;
			},
			error => { 
				console.log(error);
			});

		// this.episodioService.PorPrograma(this.programa.Id, this.PageIndex, this.PageSize, "")
		// 	.subscribe(respuesta => {
		// 		this.programa.Episodios = respuesta.Result;
		// 	},
		// 	error => { console.log(error) });
	}

	goBack(){
		console.log('back');
		 this.nav.pop();
	}

	ObtenerImagen(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-256|width-453|mode-crop|quality-100|';
	}

	ObtenerImagenPrograma(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-40|width-40|mode-crop|quality-100|';
	}

	ObtenerImagenProgramaPrincipal(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-80|width-80|mode-crop|quality-100|';
	}

	seguirPrograma(programa){
		this.ga.trackEvent('Programa', 'Favorito');
		programa.EsFavorito = !programa.EsFavorito;
		if(programa.EsFavorito)
			programa.Seguidores = programa.Seguidores + 1;
		else
			programa.Seguidores = programa.Seguidores - 1;
		
		if(programa.EsFavorito)
			this.oneSignal.sendTag(programa.Id,'favorito');
		else
			this.oneSignal.deleteTag(programa.Id);
			
		this.programaService.seguirPrograma(programa.Id, programa.EsFavorito)
			.subscribe(respuesta => {
				
			},
			error => { console.log(error) });
	}

	doRefresh(refresher) {
		
		this.PageIndex = 1;	
		this.programaService.porId(this.programa.Id)
			.subscribe(respuesta => {
				this.programa = respuesta.Result;
				refresher.complete();
			},
			error => { 
				console.log(error);
				refresher.complete();
			});
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

	bookmark(item) {
		
		this.ga.trackEvent('Novedades', 'Bookmark');
		item.Bookmark = !item.Bookmark;

		this.episodioService.Bookmark(item.Id, item.Bookmark)
			.subscribe(respuesta => {

			},
			error => { console.log(error) });
	}

		share(episodio: any) {
			this.ga.trackEvent('Novedades', 'Share');
		this.socialSharing.shareWithOptions({ url: this._sharedService.Core + '/FrontEnd/Reproducir/' + episodio.Id })
	}

	public itemTapped(item) {
		this.nav.push(VideoItem, {
			item: item
		});
	}
		doInfinite(): Promise<any> {

		return new Promise((resolve) => {

			this.PageIndex = this.PageIndex + 1;

			this.episodioService.PorPrograma(this.programa.Id, this.PageIndex, this.PageSize, "")
			.subscribe(respuesta => {
				this.ga.trackEvent('ProgramaPerfil', 'ObtenerMasEpisodios');
				//this.programa.Episodios = respuesta.Result;
				for (var i = 0; i < respuesta.Result.length; i++) {
						this.programa.Episodios.push(respuesta.Result[i]);
					}
					console.log('Finaliza la carga ' + this.PageIndex);
					resolve();
			},
			error => { console.log(error) });

		})
	}
}

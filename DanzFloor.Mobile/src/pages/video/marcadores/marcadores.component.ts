import { Component, OnInit } from '@angular/core';
import { NavController } from 'ionic-angular';
import { EntornoConfig } from "../../../constants/entorno.config";
import { VimeoService } from '../../../services/vimeo.service';
import { EpisodioService } from '../../../services/episodio.service';
import { VideoItem } from '../videoItem/videoItem.component'
import { SharedService } from "../../../services/shared.service";
import { SocialSharing } from '@ionic-native/social-sharing';
import { CanalItem } from '../canalItem/canalItem.component';
import { GoogleAnalytics } from '@ionic-native/google-analytics';


@Component({
    selector: 'marcadores',
    templateUrl: './marcadores.component.html',
	providers: [VimeoService, EpisodioService, SharedService,EntornoConfig]
})
export class MarcadoresComponent implements OnInit {
    private service: VimeoService;
	private nav: NavController;
	    private sharedService: SharedService;

	public episodios: any[] = [];
	
	PageIndex = 1;
	PageSize = 3;
	Pages = null;

	constructor(service: VimeoService, nav: NavController, 
		private episodioService: EpisodioService,		
		private socialSharing: SocialSharing,
		private entornoConfig: EntornoConfig,
		private _sharedService: SharedService,
		private ga: GoogleAnalytics) {
		this.service = service;
		this.nav = nav;
	}

	ngOnInit(): void {
		this.ga.trackView('View: Bookmarks');
		this.episodioService.EpisodisBookmark(this.PageIndex, this.PageSize)
            .subscribe(respuesta => {
				if(respuesta.Result != "")
					this.episodios = respuesta.Result;
            },
            error => { console.log(error) });
	}

	ObtenerImagen(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-256|width-453|mode-crop|quality-100|';
	}

	ObtenerImagenPrograma(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-40|width-40|mode-crop|quality-100|';
	}

	public itemTapped(item) {
		this.nav.push(VideoItem, {
			item: item
		});
	}

	public itemTappedPrograma(item) {
		this.nav.push(CanalItem, {
			item: item
		});
	}

	like(item){
		 this.ga.trackEvent('Bookmark', 'Like');
		item.Like = !item.Like;

		if(item.Like)
			item.CantidadLikes = item.CantidadLikes + 1;
		else
			item.CantidadLikes = item.CantidadLikes - 1;
		
		this.episodioService.Like(item.Id, item.Like)
            .subscribe(respuesta => {
                
            },
            error => { console.log(error) });
	}

	bookmark(item){
 		this.ga.trackEvent('Bookmark', 'Bookmark');
		item.Bookmark = !item.Bookmark;
		
		this.episodioService.Bookmark(item.Id, item.Bookmark)
            .subscribe(respuesta => {
                
            },
            error => { console.log(error) });	
		
		var index = this.episodios.indexOf(item, 0);
		if (index > -1) {
			this.episodios.splice(index, 1);
		}
	}

	share(episodio: any) {
		this.ga.trackEvent('Bookmark', 'Share');
		this.socialSharing.shareWithOptions({ url: this._sharedService.Core + '/FrontEnd/Reproducir/' + episodio.Id })
	}

	doRefresh(refresher) {
		this.ga.trackEvent('Bookmark', 'Refresh');
		this.PageIndex = 1;
		this.episodioService.EpisodisBookmark(this.PageIndex, this.PageSize)
            .subscribe(respuesta => {
				if(respuesta.Result != "")
					this.episodios = respuesta.Result;
				else
					this.episodios = [];
				refresher.complete();
            },
            error => { console.log(error) });
	}

	doInfinite(): Promise<any> {

		return new Promise((resolve) => {

			this.PageIndex = this.PageIndex + 1;
			
			if(this.Pages != null && this.PageIndex > this.Pages)
				{
					resolve();
					return;
				}
			
			console.log('Inicia la carga ' + this.PageIndex);
            
		this.episodioService.EpisodisBookmark(this.PageIndex, this.PageSize)
			.subscribe(respuesta => {
					this.ga.trackEvent('Bookmark', 'ObtenerMasBookmarks');
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
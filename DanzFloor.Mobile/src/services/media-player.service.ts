import { Injectable } from '@angular/core';

import 'rxjs/add/operator/map';
import 'rxjs/Rx';

import { SharedService } from "../services/shared.service";

import { ScreenOrientation } from '@ionic-native/screen-orientation';

//import { LogEventoService } from "../services/log-eventos.service";

declare var jwplayer: any;

@Injectable()
export class MediaPlayerService {
  constructor(private screenOrientation: ScreenOrientation,  private sharedService: SharedService) {
  }

  loadMedia(media, isAutoPlay, playerDiv) {
    var service = this.sharedService;
    this.sharedService.LogEvento( media.Identificador, 9, media.Id,2).subscribe(respuesta => {
        console.log("guardado log video");
			},
			error => { 
				console.log(error);
      });;
      
    var screenOrientation = this.screenOrientation;
    var cfg = {
      title: media.Title,
      //mediaid: media.Id,
      "preload": "none",
      "autostart": false,
      "controls": true,
      "mute": false,
      "useAudioTag": true,
      "file": media.Location,
      "skin": "bekle",
      "stretching": "exactfit",
      "width": "100%",
      "volume": 50,
      "aspectratio": "16:9",
      //image: "assets/logo.png",
      "primary": "html5",
      hlshtml: true,
      "image":  service.GetCore() + '/archivo/ObtenerImagenPorId/'+ media.Thumbnail,
      enableFullscreen: 'false',
      "logo": {
        hide: true
      },
      events:{
        onComplete: function() {
            service.LogEvento( media.Identificador, 11, media.Id,2).subscribe(respuesta => {
            console.log("guardado log video finalizado");
			  },
			  error => { 
				  console.log(error);
        });;
      },
        onPause: function() {
              var backbutton = document.getElementById(media.BackButton);
              if (backbutton != null)
                backbutton.style.visibility = "visible"
         },
        onPlay: function() {
              service.EstablecerUltimoReproductorActivo(media.PlayerId);
              var backbutton = document.getElementById(media.BackButton);
              if (backbutton != null)
                backbutton.style.visibility = "hidden"
         },  
      }
    };

    var backbutton = document.getElementById(media.BackButton);
    if (backbutton != null)
        backbutton.style.visibility = "hidden"

    return Promise.resolve(      
      jwplayer(playerDiv).setup(cfg).on('fullscreen',function(state){

         //this.logService.LogEvento("Reproducir", 9, cfg.title ,2 );
          
          console.log(state.fullscreen);
          if(state.fullscreen == true)
            screenOrientation.lock(screenOrientation.ORIENTATIONS.LANDSCAPE);
          else{
            screenOrientation.lock(screenOrientation.ORIENTATIONS.PORTRAIT);
          }
      }))
      .then(
      playerInstance => {

        if (isAutoPlay) {
          setTimeout(() => {
            return playerInstance.play();
          }, 500);
        }
      }
      )
  }

  stopMedia(playerDiv) {
    if (playerDiv != null){
    var backbuttonId = playerDiv.replace('MyMediaDiv','videoBackButton');
    var backbutton = document.getElementById(backbuttonId);
    if (backbutton != null)
        backbutton.style.visibility = "visible"
    }
    if (jwplayer(playerDiv) != null && playerDiv != "myMediaDiv")
      try{jwplayer(playerDiv).stop();}catch(e){}
      
      return null;
  }

}

    $(document).ready(function () {

       

        $(".btnMostrarDivDetalle").click(function () {
            $(".btnMostrarDivEpisodios").removeClass("active");
            $(".btnMostrarDivDetalle").addClass("active");
            $(".mostrarDivEpisodios").hide()
            $(".mostrarDivDetalle").fadeIn()
        });

        $(".btnMostrarDivEpisodios").click(function () {
            $(".btnMostrarDivDetalle").removeClass("active");
            $(".btnMostrarDivEpisodios").addClass("active");
            $(".mostrarDivDetalle").hide()
            $(".mostrarDivEpisodios").fadeIn()
        });
        
        

    });
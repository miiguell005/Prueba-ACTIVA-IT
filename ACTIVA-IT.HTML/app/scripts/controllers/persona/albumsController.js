
angular.module('cursos')
    .controller('albumsController', function ($window, $routeParams, userService, SAlbumes) {


        var vm = this;

        vm.idUsuario = 1;
        vm.pagina = 1;
        vm.cantidad = 0;

        //Contiene el título de la página 
        vm.titulo = "Albums";

        vm.albums = [];

        vm.init = function () {
            console.log(amplify.store.sessionStorage("idUsuario"));

            if (amplify.store.sessionStorage("idUsuario"))
                vm.idUsuario = amplify.store.sessionStorage("idUsuario");
            
            SAlbumes.query({ id: vm.idUsuario, pagina: vm.pagina }, function (dataAlbums) {

                vm.albums = dataAlbums.albums;
                vm.cantidad = dataAlbums.cantidad;

            }, function (error) {
                vm.manejarExcepciones(error);
            });
        }

        /**
         * Direcciona a la pagina de las canciones que posee el album
         * @param {any} idAlbum
         */
        vm.seleccionarAlbum = function (idAlbum) {

        }

        /**
         * Actualiza el id del usuario
         */
        vm.administrarIdUsuario = function () {

            if (vm.idUsuario == undefined || vm.idUsuario == null) {

                vm.idUsuario = 1;
                amplify.store.sessionStorage("idUsuario", 1);
                return;
            }

            else
                amplify.store.sessionStorage("idUsuario", vm.idUsuario);

            vm.init();
        }

        /**
         * Controla el mensaje de error
         * @param {any} error
         */
        vm.manejarExcepciones = function (error) {

            if (error && error.data.error)
                toastr.error(error.data.error);

            else
                toastr.error(error.toString());

            console.log("Error", error);
        }

        vm.irAtras = function () {
            $window.history.back();
        }

        vm.init();
    });
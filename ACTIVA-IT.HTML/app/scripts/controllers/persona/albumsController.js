
angular.module('cursos')
    .controller('albumsController', function ($window, $routeParams, userService, SAlbumes, SUsuario) {


        var vm = this;

        vm.usuario;
        vm.pagina = 1;
        vm.cantidad = 0;

        //Contiene el título de la página 
        vm.titulo = "Albums";

        //Contiene todos los uaurios guardados
        vm.usuarios = [];

        vm.albums = [];

        vm.init = function () {

            if (vm.usuarios.length == 0)
                SUsuario.query({}, function (dataUsuarios) {

                    vm.usuarios = dataUsuarios;
                    
                    if (!amplify.store.sessionStorage("idUsuario"))
                        vm.usuario = vm.usuarios[0];
                    else
                        vm.usuario = _.find(vm.usuarios, function (dataUsuario) { return dataUsuario.idUsuario == amplify.store.sessionStorage("idUsuario").idUsuario; }); 

                    SUsuario.getToken({ id: vm.usuario.idUsuario }, function (dataToken) {

                        amplify.store.sessionStorage("user", { "token": dataToken.token });
                        amplify.store.sessionStorage("idUsuario", { "idUsuario": vm.usuario.idUsuario });
                        vm.init();

                    })
                });

            //Hay un usuario seleccionado
            else {

                SUsuario.getToken({ id: vm.usuario.idUsuario }, function (dataToken) {

                    amplify.store.sessionStorage("user", { "token": dataToken.token });
                    amplify.store.sessionStorage("idUsuario", { "idUsuario": vm.usuario.idUsuario });

                    SAlbumes.query({ pagina: vm.pagina }, function (dataAlbums) {

                        vm.albums = dataAlbums.albums;
                        vm.cantidad = dataAlbums.cantidad;

                    }, function (error) {
                        vm.manejarExcepciones(error);
                    });
                });
            }
        }

        /**
         * Direcciona a la pagina de las canciones que posee el album
         * @param {any} idAlbum
         */
        vm.seleccionarAlbum = function (idAlbum) {

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
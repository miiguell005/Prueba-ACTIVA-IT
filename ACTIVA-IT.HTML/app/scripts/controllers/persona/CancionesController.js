
angular.module('cursos')
    .controller('cancionesController', function ($window, $routeParams, userService, SCanciones) {


        var vm = this;

        //Contiene todas las canciones del album
        vm.copiaCanciones = [];
        vm.canciones = [];

        vm.init = function () {

            SCanciones.query({ id: $routeParams.idAlbum, idUsuario: amplify.store.sessionStorage("idUsuario") }, function (dataCursos) {

                vm.canciones = dataCursos;
                vm.copiaCanciones = angular.copy(vm.canciones);

            }, function (error) {
                vm.manejarExcepciones(error);
            });
        }

        /**
         * Edita la categoria de la cancion
         * @param {any} faboritos
         * @param {any} inapropiada
         * @param {any} noListar
         */
        vm.editarCategoria = function (faborito, inapropiado, noListar, c) {

            SCanciones.post({ idUsuario: amplify.store.sessionStorage("idUsuario"), faborito: faborito, inapropiado: inapropiado, noListar: noListar }, c, function (dataCancion) {

                c.categoria = dataCancion.categoria;

                var copiaC = _.find(vm.copiaCanciones, function (_c) { return _c.id == dataCancion.id; });
                copiaC.categoria = dataCancion.categoria;

            }, function (error) {
                vm.manejarExcepciones(error);
            });
        }

        /**
         * Mini filtro de las canciones
         * @param {any} filtro
         */
        vm.mostrarCanciones = function (filtro) {

            if (filtro == undefined)
                vm.canciones = angular.copy(vm.copiaCanciones);

            else if (filtro != undefined)
                vm.canciones = _.filter(vm.copiaCanciones, function (c) { return c.categoria == filtro; });
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
        }

        /**
         * Regresa a la vista anterior
         */
        vm.irAtras = function () {
            $window.history.back();
        }

        vm.init();
    });
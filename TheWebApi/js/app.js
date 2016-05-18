(function() {
    'use strict';

    angular.module('app', [])
        .factory('api', ['$log', '$http', longRunningService])
        .controller('MainController', ['$log', 'api', '$interval', '$scope', mainController]);

    function longRunningService($log, $http) {
        return {
            start: start,
            stop: stop,
            getState: getState
        };

        function start() {
            return $http.post('api/start');
        }

        function stop() {
            return $http.post('api/stop');
        }

        function getState() {
            return $http.get('api/state')
                .then(function(result) {
                    return result.data;
                });
        }
    }

    function mainController($log, api, $interval, $scope) {
        var vm = this,
            _interval = null;

        vm.start = start;
        vm.stop = stop;
        vm.getState = getState;

        $scope.$on('$destroy',
            function () {
                if (_interval !== null) {
                    cancelInterval();
                }
            });

        function start() {
            vm.status = 'starting';
            api.start().then(function() {
                vm.status = 'started';
                _interval = $interval(getState, 5000);
            }, onError);
        }

        function stop() {
            vm.status = 'stopping';
            api.stop().then(function() {
                vm.status = 'stopped';
                cancelInterval();
            }, onError);
        }

        function getState() {
            api.getState()
                .then(function (result) {
                    vm.state = result;
                }, onError);
        }

        function onError(error) {
            vm.status = 'failed';
            vm.error = error;
        }

        function cancelInterval() {
            $interval.cancel(_interval);
            _interval = null;
        }
    }
})();
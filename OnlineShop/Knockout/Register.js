$(function () {
    ko.applyBindings(RegisterUser)
});

var Display = function () {
    var self = this;
    self.StateId = ko.observable();
    self.CityId = ko.observable();
    
}
self.StateId.subscribe(function () {
    self.CityId(undefined);
});
$(document).ready(function () {
    vm = new StateCityViewModel();
    ko.applyBindings(vm);


    vm.SelectedStateid.subscribe(function (newValue) {
        if (newValue !== undefined) {
            $.getJSON('getCity', { intId: newValue }, function (data) {
                vm.City(data);
            }).fail(function () {
                alert('Error getting city!');
            });
        }
        else {
            vm.City.removeAll();
            vm.SelectedCityId(undefined);
        }
    });

});
function AppViewModel() {
    var self = this;
    self.FullName = ko.observable();

}

function NewViewModel(viewModel) {
    viewModel.City = ko.observable();
    viewModel.FullName1 = ko.observable();
    viewModel.StateId = ko.observable();
    viewModel.Email.extend({ email: true });

    viewModel.getCity = function () {
        debugger;
        if (viewModel.StateId()) {
            console.log('hello' + viewModel.StateId);
            //Call controller to get the list of cities using ajax
            $.ajax({
                type: 'Get',
                url: getCitiesUrl,
                dataType: 'json',
                data: { id: viewModel.StateId() },
                success: function (data) {
                    debugger;
                    console.log(data.city);
                    viewModel.City(data.city);
                    //$.each(data.city, function (i, city) {

                    //    $("#CityId").append('<option value="'
                    //                               + city.Value + '">'
                    //                         + city.Text + '</option>');
                    //});
                },
                error: function (ex) {
                    alert('Failed.' + ex);
                }
            });

        }
    };
}

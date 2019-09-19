function AppViewModel() {
    var self = this;
    self.FullName = ko.observable();

}

function NewViewModel(viewModel) {
    viewModel.City = ko.observable();
    viewModel.FullName1 = ko.observable();
    viewModel.StateId = ko.observable();
    viewModel.Email.extend({ required: true });
   
    viewModel.UserName.extend({ required: true });
    
    
    $('#UserName').on('blur', function () {
        var data = ko.dataFor(this);
        if(data == null)
            alert("Enter UserName")
    })
    viewModel.getCity = function () {
        
        if (viewModel.StateId()) {
            console.log('hello' + viewModel.StateId);
            //Call controller to get the list of cities using ajax
            $.ajax({
                type: 'Get',
                url: getCitiesUrl,
                dataType: 'json',
                data: { id: viewModel.StateId() },
                success: function (data) {
                    
                    console.log(data.city);
                    viewModel.City(data.city);
                   
                },
                error: function (ex) {
                    alert('Failed.' + ex);
                }
            });

        }
    };
}

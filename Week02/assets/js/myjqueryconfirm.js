//Swal({
//    title: 'Are you sure?',
//    text: "You won't be able to revert this!",
//    type: 'warning',
//    showCancelButton: true,
//    confirmButtonColor: '#3085d6',
//    cancelButtonColor: '#d33',
//    confirmButtonText: 'Yes, delete it!'
//}).then((result) => {
//    if (result.value) {
//        Swal(
//            'Deleted!',
//            'Your file has been deleted.',
//            'success'
//        )
//    }
//    })

document.querySelector('#from1').addEventListener('submit', function (e) {
    var form = this;

    e.preventDefault();

    $.confirm({
        title: 'Are you sure?',
        content: 'You wont be able to revert item!',
        buttons: {
            confirm: function () {
                $.alert('Your item has been deleted.');
                form.submit();
                //let id_sp = $("#id_sp").text();

                //$.ajax({
                //    url: "",
                //    success: function () {

                //    },
                //    error: function () {
                //        alert("Something wrong !!");
                //    }
                //});
               
            },
            cancel: function () {
                $.alert('Your imaginary item is safe :)');
            }
        }
    });


    //swal({
    //    title: "Are you sure?",
    //    text: "You will not be able to recover this imaginary file!",
    //    icon: "warning",
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Yes, delete it!',
    //    dangerMode: true
    //}).then(function (isConfirm) {
    //    if (isConfirm) {
    //        swal({
    //            title: 'Deleted!',
    //            text: 'Product are successfully deleted!',
    //            icon: 'success'
    //        }).then(function () {
    //            form.submit();
    //        });
    //    } else {
    //        swal("Cancelled", "Your imaginary file is safe :)", "error");
    //    }
    //});
});
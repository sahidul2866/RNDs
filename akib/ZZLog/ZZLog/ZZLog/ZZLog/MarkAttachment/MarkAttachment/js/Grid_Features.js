$('#detail_list_resp_1').ready(function () {

    $('#detail_list_resp_1').DataTable({
        "deferRender": true,
        "order": [[3, 'desc'], [0, "desc"]],
        "iDisplayLength": 25,
        "columnDefs": [
    {
        "visible": false,
        "targets": [3]
    },
    {
        "targets": [0],
        "orderData": [3, 0]
    }
        ],
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">'
        ,
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });
});




$('#detail_list_resp').ready(function () {

    $('#detail_list_resp').DataTable({
        "deferRender": true,
        "order": [[0, "desc"], [2, 'desc']],
        "iDisplayLength": 25,
        "columnDefs": [
   {
       "visible": false,
       "targets": [0]
   },
    {
        "targets": [2],
        "orderData": [0, 2]
    }
        ],
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">'
        ,
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });
});




$('#detail_list_resp_3').ready(function () {

    $('#detail_list_resp_3').DataTable({
        "deferRender": true,
        "order": [[7, "desc"], [2, "desc"]],
        "iDisplayLength": 25,
        "columnDefs": [
   {
       "visible": false,
       "targets": [7]
   },
    {
        "targets": [2],
        "orderData": [7, 2]
    }
        ],
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">'
        //"dom": 't<"row view-pager"<"col-sm-8"<"text-center"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        ,
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });

});


$('#detail_list_resp_4').ready(function () {


    $('#detail_list_resp_4').DataTable({
        "deferRender": true,
        "order": [[4, 'desc'], [2, "desc"]],
        //"order": [[3, 'desc']],
        "iDisplayLength": 25,
        "columnDefs": [
    {
        "visible": false,
        "targets": [4]
    },
    {
        "targets": [2],
        "orderData": [4, 2]
    }
        ],
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">'
        //"dom": 't<"row view-pager"<"col-sm-8"<"text-center"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        // "dom": 't<"row view-pager"<"col-sm-2"<"text-left">><"col-sm-8"<"text-left"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        ,
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });
});


$('#detail_list_resp_0').ready(function () {

    $('#detail_list_resp_0').DataTable({
        "deferRender": true,
        "order": [[0, 'desc'], [1, "desc"]],
        //"order": [[3, 'desc']],
        "iDisplayLength": 25,
        "columnDefs": [
    {
        "visible": false,
        "targets": [0]
    },
    {
        "targets": [1],
        "orderData": [0, 1]
    }
        ],
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">'
        //"dom": 't<"row view-pager"<"col-sm-8"<"text-center"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        // "dom": 't<"row view-pager"<"col-sm-2"<"text-left">><"col-sm-8"<"text-left"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        ,
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });
});



$('#detail_list_resp_asc').ready(function () {


    $('#detail_list_resp_asc').DataTable({
        "deferRender": true,
        "order": [[0, 'asc'], [2, "asc"]],
        //"order": [[3, 'desc']],
        "iDisplayLength": 25,
        "columnDefs": [
    {
        "visible": false,
        "targets": [0]
    },
    {
        "targets": [2],
        "orderData": [0, 2]
    }
        ],
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">'
        //"dom": 't<"row view-pager"<"col-sm-8"<"text-center"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        // "dom": 't<"row view-pager"<"col-sm-2"<"text-left">><"col-sm-8"<"text-left"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        ,
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });
});


$('#detail_list_resp_5').ready(function () {

    $('#detail_list_resp_5').DataTable({
        "deferRender": true,
        "order": [[4, 'desc']],
        "iDisplayLength": 25,
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">'
        //"dom": 't<"row view-pager"<"col-sm-8"<"text-center"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        //"dom": 't<"row view-pager"<"col-sm-2"<"text-left">><"col-sm-8"<"text-left"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        ,
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });
});




$('#detail_list_respMy').ready(function () {


    $('#detail_list_respMy').DataTable({
        "deferRender": true,
        "order": [[0, "desc"], [2, 'desc']],
        "iDisplayLength": 25,
        "columnDefs": [
   {
       "visible": false,
       "targets": [0]
   },
    {
        "targets": [2],
        "orderData": [0, 2]
    }
        ],
        "dom": '<"top"<"pull-right"l><"pull-left"f>><"clear">rt<"bottom"<"center"pi>><"clear">'
        ,
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });
});


$('#myPermits').ready(function () {

    if (!($.fn.dataTable.isDataTable('#myPermits'))) {

        $('#myPermits').DataTable({            
            "deferRender": true,
            "order": [[1, 'desc'], [0, 'asc']],
            "aLengthMenu": [[25, 50, 1000, -1], [25, 50, 1000, "All"]],
            "pageLength": 50,
            "dom": '<"top"<"pull-right"l><"pull-left"f>><"clear">rt<"bottom"<"center"pi>><"clear">',
            "fnDrawCallback": function () {
                if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                    $('.dataTables_paginate').css("display", "block");
                } else {
                    $('.dataTables_paginate').css("display", "none");
                }
            },
            columnDefs: [
					{ type: 'currency', targets: 5 },
					{ type: 'date', targets: 1 }
					]   
					//bgn 05/10/17, added currency type to be able to sort properly using plug-in code in customDtCol_sort.js
					//PR 04/17/18, added type 'date'
        });

    }

});

$('#myFees').ready(function () {

    $('#myFees').DataTable({

        "deferRender": true,        
        "order": [[0, 'desc'], [1, 'asc']],
        "aLengthMenu": [[25, 50, 1000, -1], [25, 50, 1000, "All"]],
        "pageLength": 50,
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">',
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });

});


$('#cancelInspections').ready(function () {

    $('#cancelInspections').DataTable({
        "deferRender": true,
        "info": false,
        "paging": false,
        "columnDefs": [
            {
                "sortable": false,
                "targets": [0]
            }
        ],
        "oLanguage": {
            "sEmptyTable": "No inspections to cancel.  Use the links below to navigate or select \"My Inspections\" from the menu."
        },
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">',
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });
});

//must be a function call because of the way the table is loaded after the page cycle is completed
function formatFeesAndPaymentsTable() {
    
    $('#feesandpayments').DataTable({
        "order": [[0, 'desc'], [1, "desc"]],
        //"order": [[3, 'desc']],
        "iDisplayLength": 25,
        "columnDefs": [
            {
                "visible": false,
                "targets": [0]
            },
            {
                "targets": [1],
                "orderData": [0, 1]
            },
            {
                type: 'currency', targets: 6        //bgn 05/10/17, added currency type to be able to sort properly using plug-in code in customDtCol_sort.js
            }
        ],
        "dom": '<"top"<"pull-right"l>><"clear">rt<"bottom"<"center"pi>><"clear">'
        //"dom": 't<"row view-pager"<"col-sm-8"<"text-center"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
        // "dom": 't<"row view-pager"<"col-sm-2"<"text-left">><"col-sm-8"<"text-left"p>>><"row view-pager"<"col-sm-7"<"text-left"i>><"col-sm-5"<"pull-right"l>><"clearfix">>'//'rt<"bottom"i<"clear">p>', //sets the dom elements - paging, search, info
                ,
        "fnDrawCallback": function () {
            if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
                $('.dataTables_paginate').css("display", "block");
            } else {
                $('.dataTables_paginate').css("display", "none");
            }
        }
    });
    //});
}


$('#FormTable_Detail').ready(function () {

    $('#FormTable_Detail').DataTable({
        "bSort": false,
        "dom": ''
    });

});



//call from any inspection grid that needs sorting
function ApplyGridFeatures(colToSort, sortType) {	
    $('.sort').ready(function() {
        $('.sort').DataTable({
			"order": [colToSort, sortType],
			"paging": false,
			"searching": false,
			"bInfo": false
		});		
    });
}

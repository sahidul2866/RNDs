
jQuery.extend( jQuery.fn.dataTableExt.oSort, {
    "date-uk-pre": function ( a ) {
        if (a == null || a == "") {
            return 0;
        }
        var ukDatea = a.split('/');

        return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
    },

    "date-uk-asc": function ( a, b ) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "date-uk-desc": function ( a, b ) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});

//bgn 05/20/17 added these extensions to sort by total fee using the currency format
jQuery.extend( jQuery.fn.dataTableExt.oSort, {

    "currency-pre": function ( a ) {

        a = (a==="-") ? 0 : a.replace( /[^\d\-\.]/g, "");

        return parseFloat( a );

    },

 

    "currency-asc": function ( a, b ) {

        return a - b;

    },

 

    "currency-desc": function ( a, b ) {

        return b - a;

    }

} );
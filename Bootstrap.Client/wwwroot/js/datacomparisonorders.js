$(function () {
    $('#datacomparisonorders-table').lgbTable({
        url: 'api/DataComparisonOrders',
        dataBinder: {
            map: {
                Id: "#StorerKeyID",
                StorerKey: "#StorerKey",
                Facility: "#Facility",
                ExternOrderKey: "#ExternOrderKey",
                ExternLineNumber: "#ExternLineNumber",
                CustomerOrderKey: "#CustomerOrderKey",
                OrderType: "#OrderType",
                OrderDate: "#OrderDate",
                DeliveryDate: "#DeliveryDate",
                PickUpConsigneeKey: "#PickUpConsigneeKey",
                ConsigneeKey: "#ConsigneeKey",
                UrgentMark: "#UrgentMark",
                ReserveMark: "#ReserveMark",
                ColdMark: "#ColdMark",
                InvoiceNo: "#InvoiceNo",
                Notes: "#Notes",
                OTQty: "#OTQty",
                Sku: "#Sku",
                OrderQty: "#OrderQty"
            },
            callback: function (data) {
                $("#StorerKey").attr("disabled", (data && data.oper === 'edit'));
            }
        },
        smartTable: {
            singleSelect: false,
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'StorerKey',
            idField: 'StorerKey',
            queryParams: function (params) { return $.extend(params, { StorerKey: $("#txt_StorerKey").val() } ); },
            columns: [
                { title: "StorerKey", field: "StorerKey", sortable: true },
                { title: "Facility", field: "Facility", sortable: true },
                { title: "ExternOrderKey", field: "ExternOrderKey", sortable: false },
                { title: "ExternLineNumber", field: "ExternLineNumber", sortable: false },
                { title: "CustomerOrderkey", field: "CustomerOrderkey", sortable: false },
                { title: "OrderType", field: "OrderType", sortable: false },
                { title: "OrderDate", field: "OrderDate", sortable: false },
                { title: "DeliveryDate", field: "DeliveryDate", sortable: false },
                { title: "PickUpConsigneeKey", field: "PickUpConsigneeKey", sortable: false },
                { title: "ConsigneeKey", field: "ConsigneeKey", sortable: false },
                { title: "UrgentMark", field: "UrgentMark", sortable: false },
                { title: "ReserveMark", field: "ReserveMark", sortable: false },
                { title: "ColdMark", field: "ColdMark", sortable: false },
                { title: "OTQty", field: "OTQty", sortable: false },
                { title: "InvoiceNo", field: "InvoiceNo", sortable: false },
                { title: "Notes", field: "Notes", sortable: false },
                { title: "Sku", field: "Sku", sortable: false },
                { title: "OrderQty", field: "OrderQty", sortable: false }
            ]
        }
    });
});


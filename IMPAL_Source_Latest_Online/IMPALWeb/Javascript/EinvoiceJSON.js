function CreateJSON() {
    alert('hi');
    //var data = document.getElementById("ctl00_CPHDetails_hdnJSonData").value;
    //alert(document.getElementById("ctl00_CPHDetails_hdnJSonDataInv").value);
    //var temp_data = data[0];
    //var final_json = {
    //    "TranDtls": {
    //        "TaxSch": "GST",
    //        "SupTyp": "B2B",
    //        "RegRev": "N",
    //        "EcmGstin": null,
    //        "IgstonIntra": "N",
    //        "supplydir": null
    //    }
    //};
    //var DocDtls_details = {
    //    "Typ": "INV",
    //    "No": temp_data['Document_Number'],
    //    "Dt": temp_data['Document_Date']
    //}
    //final_json['DocDtls'] = DocDtls_details;
    //var SellerDtls_details = {
    //    "Gstin": "23ADDPT0274H030",
    //    "LglNm": "India Motor Parts & Accessories Limited",
    //    "TrdNm": "India Motor Parts & Accessories Limited",
    //    "Addr1": "6/8, Usha Ganj, Chhawani",
    //    "Addr2": "Indore – 452 001 - 23 - MADHYA PRADESH",
    //    "Loc": "Chhawani",
    //    "Pin": 452001,
    //    "Stcd": "23",
    //    "Ph": "9999999999",
    //    "Em": "indore@impal.net"
    //};
    //final_json['SellerDtls'] = SellerDtls_details;
    //var BuyerDtls_details = {
    //    "Gstin": temp_data['Buyer_GST'],
    //    "LglNm": temp_data['Buyer_LegalName'],
    //    "TrdNm": temp_data['Buyer_TradeName'],
    //    "Pos": temp_data['Buyer_PoS'],
    //    "Addr1": temp_data['Buyer_Address1'],
    //    "Addr2": temp_data['Buyer_Address2'],
    //    "Loc": temp_data['Buyer_Location'],
    //    "Pin": temp_data['Buyer_Pincode'],
    //    "Stcd": temp_data['Buyer_StateCode'],
    //    "Ph": temp_data['Buyer_Phone'],
    //    "Em": temp_data['Buyer_Email']
    //};
    //final_json['BuyerDtls'] = BuyerDtls_details;
    //var DispDtls_details = {
    //    "Nm": temp_data['Despatch_Name'],
    //    "Addr1": temp_data['Desptach_Address1'],
    //    "Addr2": temp_data['Despatch_Address2'],
    //    "Loc": temp_data['Despatch_Location'],
    //    "Pin": temp_data['Despatch_Pincode'],
    //    "Stcd": temp_data['Desptach_StateCode']
    //};
    //final_json['DispDtls'] = DispDtls_details;
    //var ShipDtls_Details = {
    //    "Gstin": temp_data['Shipment_GST'],
    //    "LglNm": temp_data['Shipment_LegalName'],
    //    "TrdNm": temp_data['Shipment_TradeName'],
    //    "Addr1": temp_data['Shipment_Address1'],
    //    "Addr2": temp_data['Shipment_Address2'],
    //    "Loc": temp_data['Shipment_Location'],
    //    "Pin": temp_data['Shipment_Pincode'],
    //    "Stcd": temp_data['Shipment_StateCode']
    //}
    //final_json['ShipDtls'] = ShipDtls_Details;
    //var items_list = [];
    //// for loop start
    //for (var i = 0; i < data.length; i++) {

    //    var item_dict = {
    //        "SlNo": i + 1, //data[i]["SlNo"],
    //        "PrdDesc": data[i]["PrdDesc"],
    //        "IsServc": data[i]["IsServ"],
    //        "HsnCd": data[i]["HsnCd"],
    //        "BchDtls": {
    //            "Nm": data[i]["BchDtls_Nm"],
    //            "Expdt": null,
    //            "wrDt": null
    //        },
    //        "Barcde": null,
    //        "Qty": data[i]["Qty"],
    //        "FreeQty": data[i]["FreeQty"],
    //        "Unit": data[i]["Units"],
    //        "UnitPrice": data[i]["UnitPrice"],
    //        "TotAmt": data[i]["TotAmt"],
    //        "Discount": data[i]["Discount"],
    //        "PreTaxVal": data[i]["PreTaxVal"],
    //        "AssAmt": data[i]["AssAmt"],
    //        "IgstRt": data[i]["IGST_Rate"],
    //        "IgstAmt": data[i]["IGST_Amount"],
    //        "CgstRt": data[i]["CGST_Rate"],
    //        "CgstAmt": data[i]["CGST_Amount"],
    //        "SgstRt": data[i]["SGST_Rate"],
    //        "SgstAmt": data[i]["SGST_Amount"],
    //        "CesRt": data[i]["CESS_Rate"],
    //        "CesAmt": data[i]["CESS_Amount"],
    //        "CesNonAdvlAmt": data[i]["CesNonAdvlAmt"],
    //        "StateCesRt": data[i]["StateCesRt"],
    //        "StateCesAmt": data[i]["StateCesAmt"],
    //        "StateCesNonAdvlAmt": data[i]["StateCesNonAdvlAmt"],
    //        "OthChrg": data[i]["OthChrg"],
    //        "TotItemVal": data[i]["TotItemVal"],
    //        "OrdLineRef": data[i]["OrdLineRef"],
    //        "OrgCntry": "IN",
    //        "PrdSlNo": null,
    //        "AttribDtls": [
    //            {
    //                "Nm": data[i]["AttribDtls_Nm"],
    //                "Val": data[i]["AttribDtls_Nm"]
    //            }
    //        ],
    //        "EGST": {
    //            "nilrated_amt": "0",
    //            "exempted_amt": "0",
    //            "non_gst_amt": "0",
    //            "reason": null,
    //            "debit_gl_id": "1",
    //            "debit_gl_name": "DGL",
    //            "credit_gl_id": "2",
    //            "credit_gl_name": "CGL",
    //            "sublocation": "SUBLOC"
    //        }
    //    }
    //    items_list.push(item_dict);
    //}

    //// End for loop
    //final_json['ItemList'] = { "Item": items_list };
    //var ValDtls_Details = {
    //    "AssVal": temp_data['ValDtls_AssVal'],
    //    "CgstVal": temp_data['ValDtls_CgstVal'],
    //    "SgstVal": temp_data['ValDtls_SgstVal'],
    //    "IgstVal": temp_data['ValDtls_IgstVal'],
    //    "CesVal": temp_data['ValDtls_CesVal'],
    //    "StCesVal": 0,
    //    "Discount": 0,
    //    "OthChrg": 0,
    //    "RndOffAmt": temp_data['ValDtls_RndOffAmt'],
    //    "TotInvVal": temp_data['ValDtls_TotVal'],
    //    "TotInvValFc": null
    //};
    //final_json['ValDtls'] = ValDtls_Details;
    //var PayDtls_Details = {
    //    "Nm": temp_data['PayDtls_Nm'],
    //    "Accdet": temp_data['PayDtls_Accdet'],
    //    "Mode": temp_data['PayDtls_Mode'],
    //    "Fininsbr": temp_data['PayDtls_Fininsbr'],
    //    "Payterm": temp_data['PayDtls_Payterm'],
    //    "Payinstr": temp_data['PayDtls_Payinstr'],
    //    "Crtrn": temp_data['PayDtls_Crtrn'],
    //    "Dirdr": temp_data['PayDtls_Dirdr'],
    //    "Crday": temp_data['PayDtls_Crday'],
    //    "Paidamt": temp_data['PayDtls_Paidamt'],
    //    "Paymtdue": temp_data['PayDtls_Paymtdue']
    //};
    //final_json['PayDtls'] = PayDtls_Details;
    //var RefDtls_Details = {
    //    "InvRm": "INVREM",
    //    "DocPerdDtls": {
    //        "InvStDt": temp_data['Buyer_GST'],
    //        "InvEndDt": temp_data['PayDtls_Paymtdue']
    //    },
    //    "PrecDocDtls": [
    //        {
    //            "InvNo": null,
    //            "InvDt": null,
    //            "OthRefNo": null
    //        }
    //    ],
    //    "ContrDtls": [
    //        {
    //            "RecAdvRefr": null,
    //            "RecAdvDt": null,
    //            "Tendrefr": null,
    //            "Contrrefr": null,
    //            "Extrefr": null,
    //            "Projrefr": null,
    //            "Porefr": null,
    //            "PoRefDt": null
    //        }
    //    ]
    //};
    //final_json['RefDtls'] = RefDtls_Details;
    //var AddlDocDtls_Details = [
    //    {
    //        "Url": "URL",
    //        "Docs": null,
    //        "Info": null
    //    }
    //];
    //final_json['AddlDocDtls'] = AddlDocDtls_Details;
    //var ExpDtls_Details = {
    //    "ShipBNo": null,
    //    "ShipBDt": null,
    //    "Port": null,
    //    "RefClm": null,
    //    "ForCur": null,
    //    "CntCode": null,
    //    "ExpDuty": null
    //};
    //final_json['ExpDtls'] = ExpDtls_Details;
    //var EwbDtls_Details = {
    //    "Transid": null,
    //    "Transname": null,
    //    "Distance": null,
    //    "Transdonullcno": null,
    //    "TransdocDt": null,
    //    "Vehno": null,
    //    "Vehtype": null,
    //    "TransMode": null
    //};
    //final_json['EwbDtls'] = EwbDtls_Details;
    ////console.log(final_json);
    //var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(final_json, 0, 4));
    ////console.log(dataStr);
    //document.getElementById("ctl00_CPHDetails_hdnJSonDataInv").value = dataStr;
    ////var dlAnchorElem = document.getElementById('ctl00_CPHDetails_downloadAnchorElem');
    ////console.log(dlAnchorElem);
    ////dlAnchorElem.setAttribute("href", dataStr);
    ////dlAnchorElem.setAttribute("download", "invoice.json");
    ////dlAnchorElem.click();
}
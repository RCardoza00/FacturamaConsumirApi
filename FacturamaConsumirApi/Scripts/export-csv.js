

function getDataFromTable() {
	//gets table
	var oTable = document.getElementById('tblExport');

	//gets rows of table
	var rowLength = oTable.rows.length;

	var data = [];
	//loops through rows    
	for (i = 1; i < rowLength; i++) {

		//gets cells of current row
		var oCells = oTable.rows.item(i).cells;

		data.push({
			numero: oCells.item(0).innerHTML,
			folio: oCells.item(1).innerHTML,
			emisor: oCells.item(2).innerHTML,
			rfcEmisor: oCells.item(3).innerHTML,
			receptor: oCells.item(4).innerHTML,
			rfcReceptor: oCells.item(5).innerHTML,
			fecha: oCells.item(6).innerHTML,
			total: oCells.item(7).innerHTML
		});
	}
	return data;
}

function makeCSV(data) {
	const array = [Object.keys(data[0])].concat(data);

	return array.map(item => {
		return Object.values(item).toString()
	}).join('\n');
}

function downloadCSV() {
	// Creating a Blob for having a csv file format
	// and passing the data with type
	const blob = new Blob([makeCSV(getDataFromTable())], { type: 'text/csv' });

	// Creating an object for downloading url
	const url = window.URL.createObjectURL(blob);

	// Creating an anchor(a) tag of HTML
	const a = document.createElement('a');

	// Passing the blob downloading url
	a.setAttribute('href', url);

	// Setting the anchor tag attribute for downloading
	// and passing the download file name
	a.setAttribute('download', 'CFDIs_EXPORT.csv');

	// Performing a download with click
	a.click()
}

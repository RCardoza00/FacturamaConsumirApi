

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

		if (oCells.item(0).innerText === 'no registros') {
			console.log('regresa nulo');
			return null;
        }

		data.push({
			numero: oCells.item(0).innerHTML,
			folioFiscal: oCells.item(1).innerHTML,
			serie: oCells.item(2).innerHTML,
			folio: oCells.item(3).innerHTML,
			emisor: oCells.item(4).innerHTML,
			rfcEmisor: oCells.item(5).innerHTML,
			receptor: oCells.item(6).innerHTML,
			rfcReceptor: oCells.item(7).innerHTML,
			fecha: oCells.item(8).innerHTML,
			total: oCells.item(9).innerHTML
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
	var data = getDataFromTable();
	if (data === null) {
		console.log('no hace nada');
		return;
	}
	// Creating a Blob for having a csv file format
	// and passing the data with type
	const blob = new Blob([makeCSV(data)], { type: 'text/csv' });

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

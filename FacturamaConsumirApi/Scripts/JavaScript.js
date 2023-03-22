
var inputFolioFiscal = document.getElementById("FolioFiscal");
var inputFechaInicio = document.getElementById("startDate");
var inputFechaFin = document.getElementById("endDate");
var inputRfcEmisor = document.getElementById("rfcEmisor");
var inputSerie = document.getElementById("serie");
var inputRfcReceptor = document.getElementById("rfcReceptor");
var inputFolio = document.getElementById("folio");
//funciones para habiliar los inputs de fecha y folio
function enableFolioFiscal() {
	inputFolioFiscal.disabled = "";
	inputFechaInicio.disabled = "true";
	inputFechaFin.disabled = "true";
	inputRfcEmisor.disabled = "true";
	inputSerie.disabled = "true";
	inputRfcReceptor.disabled = "true";
	inputFolio.disabled = "true";
}
function enableFechasEmision() {
	inputFolioFiscal.disabled = "true";
	inputFechaInicio.disabled = "";
	inputFechaFin.disabled = "";
	inputRfcEmisor.disabled = "";
	inputSerie.disabled = "";
	inputRfcReceptor.disabled = "";
	inputFolio.disabled = "";
}

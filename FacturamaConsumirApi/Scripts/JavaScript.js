
var inputFolio = document.getElementById("FolioFiscal");
var inputFechaInicio = document.getElementById("startDate");
var inputFechaFin = document.getElementById("endDate");
var inputRfcEmisor = document.getElementById("rfcEmisor");
var inputRfcReceptor = document.getElementById("rfcReceptor");
//funciones para habiliar los inputs de fecha y folio
function enableFolioFiscal() {
	inputFolio.disabled = "";
	inputFechaInicio.disabled = "true";
	inputFechaFin.disabled = "true";
	inputRfcEmisor.disabled = "true";
	inputRfcReceptor.disabled = "true";
}
function enableFechasEmision() {
	inputFolio.disabled = "true";
	inputFechaInicio.disabled = "";
	inputFechaFin.disabled = "";
	inputRfcEmisor.disabled = "";
	inputRfcReceptor.disabled = "";
}
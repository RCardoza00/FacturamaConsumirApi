
var inputFolioFiscal = document.getElementById("FolioFiscal");
var inputFechaInicio = document.getElementById("startDate");
var inputFechaFin = document.getElementById("endDate");
var inputRfcEmisor = document.getElementById("rfcEmisor");
var inputSerie = document.getElementById("serie");
var inputRfcReceptor = document.getElementById("rfcReceptor");
var inputFolio = document.getElementById("folio");

// Funciones para habiliar los inputs de fecha y folio
function enableFolioFiscal() {
	inputFolioFiscal.disabled = "";
	inputFechaInicio.disabled = "true";
	inputFechaFin.disabled = "true";
	inputRfcEmisor.disabled = "true";
	inputSerie.disabled = "true";
	inputRfcReceptor.disabled = "true";
	inputFolio.disabled = "true";

	inputFolioFiscal.classList.remove("disabled-input");
	inputFechaInicio.classList.add("disabled-input");
	inputFechaFin.classList.add("disabled-input");
	inputRfcEmisor.classList.add("disabled-input");
	inputSerie.classList.add("disabled-input");
	inputRfcReceptor.classList.add("disabled-input");
	inputFolio.classList.add("disabled-input");

	inputFechaInicio.style.cursor = "";
	inputFechaFin.style.cursor = "";
}

function enableFechasEmision() {
	inputFolioFiscal.disabled = "true";
	inputFechaInicio.disabled = "";
	inputFechaFin.disabled = "";
	inputRfcEmisor.disabled = "";
	inputSerie.disabled = "";
	inputRfcReceptor.disabled = "";
	inputFolio.disabled = "";

	inputFolioFiscal.classList.add("disabled-input");
	inputFechaInicio.classList.remove("disabled-input");
	inputFechaFin.classList.remove("disabled-input");
	inputRfcEmisor.classList.remove("disabled-input");
	inputSerie.classList.remove("disabled-input");
	inputRfcReceptor.classList.remove("disabled-input");
	inputFolio.classList.remove("disabled-input");

	inputFechaInicio.style.cursor = "pointer";
	inputFechaFin.style.cursor = "pointer";
}

// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  Server: 'http://localhost:4200/auth/',
  production: false,
  // APIS Modulos de Planeacion
  FCAPROGAPI001: 'http://localhost:51197/', // CATALOGOS
  FCAPROGAPI002: 'http://localhost:51198/', // DATOS
  // FCAPROGAPI001: 'http://intranet3.cecso.com.mx/ErpWebA8/Apis/Fca/Prog/FCAPROGAPI001/', // CATALOGOS
  // FCAPROGAPI002: 'http://intranet3.cecso.com.mx/ErpWebA8/Apis/Fca/Prog/FCAPROGAPI002/', // DATOS

  // OTRAS
  FCAPROGAPI001_RS: 'http://datosnv2008.cecso.com.mx/ReportServer/Pages/ReportViewer.aspx?/FcaCajas01/FCAPROG001RW/FCAPROG001RW&rs:Command=Render',
  FCAVENTAPI001: 'http://intranet3.cecso.com.mx/ErpWebA8/Apis/Fca/Vent/FCAVENTAPI001M/', //  VENTA
  ERPGRALAPI001:  'http://intranet3.cecso.com.mx/ErpWebA8/Apis/Erp/Gral/ERPGRALAPI001M/', // API GENERAL ERP
  SERTRAFAPI001: 'http://intranet3.cecso.com.mx/ErpWebA8/Apis/Ser/traf/SERTRAFAPI001M/', // API TRAFICO
  AMDCCOBAPI001: 'http://intranet3.cecso.com.mx/ErpWebA8/Apis/adm/cxc/ADMCCOBAPI001/', // API CARTERA
  URL_INFORMATICA_INFERPAPI001: 'http://172.16.2.235/ERPWebApi2020/ApisInformatica/INFERPAPI001/api/', // API REGLAS DE NEGOCIO
  URL_API_ADMFCOAPI001: 'http://172.16.2.235/ERPWebApi2020/ApisFacturacion/ADMFCOAPI001/api/', // API FACTURACION
  URLApiArchivosLocal: 'http://intranet3.cecso.com.mx/ErpWebA8/Apis/Ser/Gral/apisubirarchivos', // API ARCHIVOS (SUBIR)
  URLApiArchivos: 'https://app.cecso.com.mx/Apis/3.0/ERPCMP001_Archivos' // API ARCHIVOS (BAJAR)
  , zona:'01'
  , VERSIONCPLCAP005: '2022.02.11.1'
};

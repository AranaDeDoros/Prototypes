<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Documento sin t&iacute;tulo</title>
</head>
<?php
$archivo="REG_PRE.TXT";
$lineas=file($archivo);
$campos=explode(",",$lineas[0]);
$pres=$campos[0];
$fecha=$campos[1];
$hora=$campos[2];
$pres2=$pres/1000;
$formulario='<form method="post">
<fieldset>
<legend>Consulta de la Presión Atmosférica</legend>
<p>
<label>Presión Atmosférica(hPa)
<input name="pres" type="text" size=6 value="'.$pres.'"/>
<input name="fecha" type="text" size=9 value= "'.$fecha.'"/>
<input name="hora" type="text" size=10 value="'.$hora.'"/>
<label>
<p>
<input name="pres" type="text" size=6 value="'.$pres2.'"/> <label> kPa </label>
</p>

</p>
<p>
<label>Actualiza Valores:
<input name="Presión Atmosférica" type="submit" value="Actualiza Temperatura"/>
</label>
</p>

</fieldset>
</form>';
echo $formulario;
//exit();
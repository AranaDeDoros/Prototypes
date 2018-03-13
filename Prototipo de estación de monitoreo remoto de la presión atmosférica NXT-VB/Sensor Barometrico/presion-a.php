<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<meta content="5" http-equiv="REFRESH"> </meta>
<title>Monitoreo Remoto de la Presión Atmosférica</title>
</head>
<?php
echo "<html>\n"; 
echo "<body bgcolor=\"#ffffff\">\n"; 
echo "\n"; 
echo "<h1><font color=\"#100c08\"Consulta de la Presión Atmosférica</font></h1>\n"; 
echo "\n"; 
echo "<p><b>Descripción:</b> <i>A continuación se muestra la lectura de la presión atmosférica ambiental. En el primer recuadro se muestra con las unidades de pulgadas de mercurio (inHg) unidad del sistema inglés y la usada de manera predeterminada por el microcontrolador LEGO NXT, acompañada de la fecha y hora en que fue tomada; debajo de ésta se encuentra la misma lectura, pero en milímetros de mercurio (mmHg).</i></p>\n";
echo "\n"; 
echo "<p><b>Nota:</b><i>Para actualizar la información se tiene dos opciones:</i>\n"; 
echo "<i><ol><li>Usar el botón de actualizar localizado en la parte inferior del formulario.</li>\n"; 
echo "<li>Esperar 5 segundos a que la página se actualice por sí sola.</li></ol></i></p>\n"; 
echo "\n"; 
echo "</body>\n"; 
echo "</html>\n"; 
echo "\n";
$archivo="REG_PRE.TXT";
$lineas=file($archivo);
$campos=explode(",",$lineas[0]);
$pres=$campos[0];
$fecha=$campos[1];
$hora=$campos[2];
$pres2=($pres/1000)*25.4;
$formulario='<form method="post">
<fieldset>
<legend>Consulta de la Presión Atmosférica</legend>

<p>
<input name="pres" type="text" size=6 value="'.$pres.'"/><label>inHg</label>
<input name="fecha" type="text" size=9 value= "'.$fecha.'"/>
<input name="hora" type="text" size=10 value="'.$hora.'"/>
</p>

<p>
<input name="pres" type="text" size=6 value="'.$pres2.'"/><label>mmHg</label>
<input name="fecha" type="text" size=9 value= "'.$fecha.'"/>
<input name="hora" type="text" size=10 value="'.$hora.'"/>
</p>

<p>
<input name="Presión Atmosférica" type="submit" value="Actualizar"/>
</p>

</fieldset>
</form>';
echo $formulario;
//exit();

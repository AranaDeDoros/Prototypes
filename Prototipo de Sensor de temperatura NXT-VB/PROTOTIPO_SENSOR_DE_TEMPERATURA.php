<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Documento sin t&iacute;tulo</title>
</head>
<?php
  $archivo="REGTEMP_NXT.TXT";   //La ruta del archivo
  $lineas=file($archivo);    //lineas se convierte en automático en una matriz con tantas filas como renglones tiene el archivo nombres
  $campos=explode(",",$lineas[0]);  //Convierte a una matriz separada por comas
  $temp=$campos[0];  //Lee la primera posición de la matriz, es decir, la temperatura
  $fecha=$campos[1];   //Lee la segunda posición de la matriz, es decir, la fecha en que fue recibida la temperatura
  $hora=$campos[2];  //Lee la tercera posición de la matriz, es decir, la hora en que fue recibida la temperatura
  //Almacena en la variable $formulario toda la sintáxis de html para ser ejecutada posteriormente
  $formulario='<form method="post">   
  <fieldset>
  <legend>Consulta de la temperatura:</legend>
  <p>
  <label>Temperatura:	 
  <input name="temp" type="text" size=6 value="'.$temp.'"/> <label> °Celsius </label>
  <input name="fecha" type="text" size=9 value="'.$fecha.'"/>
  <input name="hora" type="text" size=10 value="'.$hora.'"/>
  </label>
  </p> 
  <p>
  <label>Actualiza Valores:	 
  <input name="Temperatura" type="submit" value="Actualiza Temperatura"/>
  </label>
  </p>    	
  
 </fieldset>
 </form>';
 echo $formulario;    //Ejecuta el contenido de la variable $formulario en código HTML
?>
<body>
</body>
</html>
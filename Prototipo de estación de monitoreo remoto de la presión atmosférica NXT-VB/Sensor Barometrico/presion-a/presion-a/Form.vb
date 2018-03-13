Public Class Form1
    Dim bandera As Boolean = False
    Dim ubicacion As String = "C:/xampp/htdocs/Sensor_Barometrico/REG_PRE.TXT"

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim fecha, hora As String
        Dim byteIn(65) As Byte
        Dim byteOut(7) As Byte
        Dim i, tam As Integer
        Dim empezar, finalizar, tiempoactual As Double
        With SerialPort1
            .PortName = "COM140"  'puerto para comunicarse a trav�s del Bluetooth
            .BaudRate = 9600 'velocidad maxima de transmision
            .Parity = IO.Ports.Parity.None ' sin paridad
            .DataBits = 8 ' bits de datos
            .StopBits = IO.Ports.StopBits.One ' un bit de parada
            .ReadTimeout = 300  '300ms tiempo de lectura
            .WriteTimeout = 300 '300ms tiempo de escritura
        End With
        SerialPort1.Open() 'Abre la comunicaci�n a trav�s del puerto
        Label3.Text = ""  'Limpia el contenido de la etiqueta Label3
        Label3.Text = "Conectado"  'Mostrar status de bluetooth como conectado
        Refresh()  'Actualiza la informaci�n del formulario
        While bandera <> True    'mientras bandera sea diferente de verdadero...
            'SOLICITA PRESION AL NXT utilizando el comando MESSAGEREAD (13)
            byteOut(0) = &H5  'numero de bytes en el mensaje de salida
            byteOut(1) = &H0  'debe ser 0 para el NXT byte mas significativo del tama�o del mensaje
            byteOut(2) = &H0 '&H0 = esperando respuesta &H80 = sin esperar respuesta
            byteOut(3) = &H13 'Leer mensaje
            byteOut(4) = &HA  'n�mero de buz�n de entrada remoto(0-9) + 9 ejemplo: para inbox 1 escribir A
            byteOut(5) = &H0  'n�mero de buz�n de entrada local(0-9)
            byteOut(6) = &H0  'Remover?(Boolean: VERDADERO(diferente de cero) limpia el valor del mensaje del buz�n de entrada Remoto)
            SerialPort1.Write(byteOut, 0, 7) 'envia mensaje

            empezar = Microsoft.VisualBasic.DateAndTime.Timer
            finalizar = empezar + 0.2
            tiempoactual = Microsoft.VisualBasic.DateAndTime.Timer
            Do While tiempoactual < finalizar    'Retardo de doscientos milisegundos
                tiempoactual = Microsoft.VisualBasic.DateAndTime.Timer
            Loop
            'RECIBE PRESION DEL NXT
            byteIn(0) = SerialPort1.ReadByte 'N�mero de bytes incluidos en el paquete del mensaje (byte menos significativo - LSB 40H = 64bytes)
            byteIn(1) = SerialPort1.ReadByte 'N�mero de bytes incluidos en el paquete del mensaje (byte mas significativo - MSB 0H)
            byteIn(2) = SerialPort1.ReadByte 'Recibe un valor de 2 indicando que es una respuesta del NXT
            byteIn(3) = SerialPort1.ReadByte 'Recibe un valor de 13 (numero de comando de lectura)
            byteIn(4) = SerialPort1.ReadByte 'Status Byte
            byteIn(5) = SerialPort1.ReadByte 'N�mero de buz�n local (0-9)
            byteIn(6) = SerialPort1.ReadByte 'Tama�o del mensaje
            tam = byteIn(6) 'Guarda el tama�o del mensaje en la variable tam
            TextBox1.Text = "" 'Limpia el contenido del cuadro de texto TextBox1
            'Lee los siguientes bytes correspondientes al mensaje recibido
            'For i = 7 To tam + 6
            For i = 1 To tam
                TextBox1.Text = TextBox1.Text + Chr(SerialPort1.ReadByte)   'Guarda el mensaje en el cuadro de texto
            Next
            TextBox2.Text = (TextBox1.Text / 1000) * 25.4
            For i = 7 + tam To 65
                byteIn(i) = SerialPort1.ReadByte
            Next
            Refresh()
            fecha = Format(Now, "dd/MM/yyyy")  'Guarda la fecha de la lectura de la presi�n en formato dia/mes/a�o
            hora = Microsoft.VisualBasic.DateAndTime.TimeOfDay      'Guarda la hora de la lectura de la presi�n
            'Guarda en un archivo la lectura de la presi�n recibida
            'Un valor de True agrega la informaci�n al final del archivo, un valor de False sobreescribe la informaci�n anterior 
            My.Computer.FileSystem.WriteAllText(ubicacion, TextBox1.Text & "," & fecha & "," & hora & vbCrLf, True, System.Text.Encoding.Default)
        End While
    End Sub

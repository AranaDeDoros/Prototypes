Public Class Form1
    Public contador As Integer = 0
    Dim bandera As Boolean = False
    Dim bandera2 As Boolean = True
    Dim ubicacion As String = "C:/xampp/htdocs/Sensor_Barometrico/REG_PRE.TXT"

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim fecha, hora As String
        Dim byteIn(65) As Byte
        Dim byteOut(7) As Byte
        Dim i, tam As Integer
        Dim empezar, finalizar, tiempototal As Double
        With SerialPort1
            .PortName = "COM35"  'puerto para comunicarse a través del
            'Bluetooth
            .BaudRate = 96000 'velocidad maxima de transmision
            .Parity = IO.Ports.Parity.None ' sin paridad
            .DataBits = 8 ' bits de datos
            .StopBits = IO.Ports.StopBits.One ' un bit de parada
            .ReadTimeout = 300  '300ms tiempo de lectura
            .WriteTimeout = 300 '300ms tiempo de escritura
        End With
        While bandera <> True   ' mientras bandera sea verdadero
            SerialPort1.Open()  'abre la comunicacion a traves del puerto
            Label3.Text = ""    'serial por Bluetooth
            Label3.Text = "Conectado" 'mostrar status de Bluetooth como
            'conectado
            Refresh() 'actualiza la informacion del formulario
            empezar = Microsoft.VisualBasic.DateAndTime.Timer
            finalizar = empezar + 1
            tiempototal = Microsoft.VisualBasic.DateAndTime.Timer
            Do While tiempototal < finalizar  ' retardo de un segundo
                tiempototal = Microsoft.VisualBasic.DateAndTime.Timer
            Loop
            'SOLICITA PRESIÓN ATMOSFÉRICA AL NXT
            byteOut(0) = &H5  'numero de bytes en el mensaje de salida
            byteOut(1) = &H0  'debe ser 0 para el NXT
            byteOut(2) = &H0 '&H0=esperando respuesta &H80=sin esperar
            'respuesta
            byteOut(3) = &H13 'leer mensaje
            byteOut(4) = &HA  'número de buzón de entrada remoto(0-9) + 9
            'ejemplo: para inbox 1 escribir A
            byteOut(5) = &H0  'número de buzón de entrada local (0-9)
            byteOut(6) = &H0  'Remover?(Boolean: VERDADERO(diferente de cero) 
            'limpia el valor del mensaje desde el buzón de entrada remoto)
            SerialPort1.Write(byteOut, 0, 7) 'envía mensaje
            empezar = Microsoft.VisualBasic.DateAndTime.Timer
            finalizar = empezar + 1
            tiempototal = Microsoft.VisualBasic.DateAndTime.Timer
            Do While tiempototal < finalizar  ' retardo de un segundo.
                tiempototal = Microsoft.VisualBasic.DateAndTime.Timer
            Loop
            'RECIBE TEMPERATURA DEL NXT
            byteIn(0) = SerialPort1.ReadByte
            byteIn(1) = SerialPort1.ReadByte
            byteIn(2) = SerialPort1.ReadByte 'Recibe un valor de 2 indicando 
            'que es una respuesta del Lego NXT
            byteIn(3) = SerialPort1.ReadByte 'Recibe un valor de 13 (numero de 
            'comando de lectura)
            byteIn(4) = SerialPort1.ReadByte 'Status Byte
            byteIn(5) = SerialPort1.ReadByte 'Número de buzón local (0-9)
            byteIn(6) = SerialPort1.ReadByte 'Tamaño del mensaje
            tam = byteIn(6) 'guarda el tamaño del mensaje en la variable tam
            TextBox1.Text = ""
            'Lee los siguientes bytes correspondientes al mensaje recibido
            For i = 7 To tam + 7
                'Guarda el mensaje en el cuadro de texto
                TextBox1.Text = TextBox1.Text + Chr(SerialPort1.ReadByte)
            Next
            Refresh()
            fecha = Format(Now, "dd/MM/yyyy")    'Guarda la 
            'fecha de la lectura de la temperatura
            hora = Microsoft.VisualBasic.DateAndTime.TimeOfDay      'Guarda la 
            'hora de la lectura de la temperatura
            SerialPort1.Close()  'Cierra el puerto utilizado para bluetooth
            Label3.Text = "Desconectado"
            Refresh()
            empezar = Microsoft.VisualBasic.DateAndTime.Timer
            finalizar = empezar + 1
            tiempototal = Microsoft.VisualBasic.DateAndTime.Timer
            Do While tiempototal < finalizar  ' retardo de un segundo
                tiempototal = Microsoft.VisualBasic.DateAndTime.Timer
            Loop
            'Guarda en un archivo la lectura de la temperatura recibida
            My.Computer.FileSystem.WriteAllText(ubicacion, TextBox1.Text & "," & fecha & "," & hora & "," & vbCrLf, False, System.Text.Encoding.Default)
            'Cierra el puerto utilizado para la comunicación bluetooth
            SerialPort1.Close()
            Label3.Text = "Desconectado"
            Refresh()
            empezar = Microsoft.VisualBasic.DateAndTime.Timer
            finalizar = empezar + 1
            tiempototal = Microsoft.VisualBasic.DateAndTime.Timer
            Do While tiempototal < finalizar  ' retardo de un segundo.
                tiempototal = Microsoft.VisualBasic.DateAndTime.Timer
            Loop
        End While
    End Sub


End Class

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim fecha As String
        Dim hora As String

        Dim byteIn(65) As Byte
        Dim byteOut(7) As Byte
        Dim i, tam As Integer
        Dim start, finish, totaltime As Double

        With SerialPort1
            .PortName = "COM46"  '<<<< Change this number for your computer
            .BaudRate = 96000
            .Parity = IO.Ports.Parity.None
            .DataBits = 8
            .StopBits = IO.Ports.StopBits.One
            .ReadTimeout = 300  '300ms
            .WriteTimeout = 300 '300ms
        End With

        While bandera <> True
            SerialPort1.Open()
            Label1.Text = ""
            Label1.Text = "Connected"
            Refresh()

            ' Set end time for 2-second duration.
            start = Microsoft.VisualBasic.DateAndTime.Timer
            finish = start + 1
            totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Do While totaltime < finish
                totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Loop

            byteOut(0) = &H5  'number bytes in output message
            byteOut(1) = &H0  'should be 0 for NXT
            byteOut(2) = &H0 '&H0 = reply expected &H80 = no reply expected
            byteOut(3) = &H13 'Message Read
            byteOut(4) = &HA  'Remote Inbox number (0-9) + 9 ejemplo: para inbox 1 escribir A
            byteOut(5) = &H0  'Local Inbox number (0-9)
            byteOut(6) = &H0  'Remove?(Boolean: TRUE(non zero) value clears message from Remote Inbox)
            SerialPort1.Write(byteOut, 0, 7) 'send message
            ' Set end time for 1-second duration.
            start = Microsoft.VisualBasic.DateAndTime.Timer
            finish = start + 1
            totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Do While totaltime < finish
                '' Do other processing while waiting for 1 second to elapse
                totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Loop

            byteIn(0) = SerialPort1.ReadByte
            byteIn(1) = SerialPort1.ReadByte
            byteIn(2) = SerialPort1.ReadByte
            byteIn(3) = SerialPort1.ReadByte
            byteIn(4) = SerialPort1.ReadByte
            byteIn(5) = SerialPort1.ReadByte
            byteIn(6) = SerialPort1.ReadByte
            tam = byteIn(6)
            '
            'Hace una segunda petición para leer ahora el valor de la temperatura
            'SerialPort1.Write(byteOut, 0, 7) 'send message

            'TextBox4.Text = byteIn(0)
            'TextBox5.Text = byteIn(1)
            'TextBox6.Text = byteIn(2)
            'TextBox7.Text = byteIn(3)
            'TextBox8.Text = byteIn(4)
            'TextBox9.Text = byteIn(5)
            'TextBox10.Text = byteIn(6)

            'TextBox11.Text = SerialPort1.ReadByte
            'TextBox12.Text = SerialPort1.ReadByte
            'TextBox13.Text = SerialPort1.ReadByte
            'TextBox14.Text = SerialPort1.ReadByte
            'TextBox15.Text = SerialPort1.ReadByte
            'TextBox16.Text = SerialPort1.ReadByte
            'TextBox17.Text = SerialPort1.ReadByte
            'TextBox18.Text = SerialPort1.ReadByte
            'TextBox19.Text = SerialPort1.ReadByte
            'TextBox20.Text = SerialPort1.ReadByte
            'TextBox21.Text = SerialPort1.ReadByte

            'For i = 0 To 64
            'byteIn(i) = 0
            'Next
            TextBox22.Text = ""
            'Lee los siguientes bytes correspondientes al mensaje recibido
            For i = 7 To tam + 7
                'byteIn(i) = SerialPort1.ReadByte
                TextBox22.Text = TextBox22.Text + Chr(SerialPort1.ReadByte)
            Next

            Refresh()
            fecha = Microsoft.VisualBasic.DateAndTime.DateString
            hora = Microsoft.VisualBasic.DateAndTime.TimeOfDay
            'Guarda en un archivo la lectura de temperatura recibida
            'My.Computer.FileSystem.WriteAllText(ubicacion2, TextBox22.Text & " " & fecha & " " & hora & vbCrLf, False)

            'Cierra el puerto utilizado para la comunicación bluetooth
            SerialPort1.Close()
            Label1.Text = "Disconnected"
            Refresh()
            ' Set end time for 1-second duration.
            start = Microsoft.VisualBasic.DateAndTime.Timer
            finish = start + 1
            totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Do While totaltime < finish
                ' Do other processing while waiting for 5 seconds to elapse.
                totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Loop

            'PROCESO DE OBTENCIÓN DE LA PRESIÓN ATMOSFÉRICA
            SerialPort1.Open()
            Label1.Text = ""
            Label1.Text = "Connected"
            Refresh()
            ' Set end time for 2-second duration.
            start = Microsoft.VisualBasic.DateAndTime.Timer
            finish = start + 1
            totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Do While totaltime < finish
                totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Loop

            byteOut(0) = &H5  'number bytes in output message
            byteOut(1) = &H0  'should be 0 for NXT
            byteOut(2) = &H0 '&H0 = reply expected &H80 = no reply expected
            byteOut(3) = &H13 'Message Read
            byteOut(4) = &HB  'Remote Inbox number (0-9) + 9 ejemplo: para inbox 1 escribir A
            byteOut(5) = &H1  'Local Inbox number (0-9)
            byteOut(6) = &H0  'Remove?(Boolean: TRUE(non zero) value clears message from Remote Inbox)
            SerialPort1.Write(byteOut, 0, 7) 'send message
            ' Set end time for 1-second duration.
            start = Microsoft.VisualBasic.DateAndTime.Timer
            finish = start + 1
            totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Do While totaltime < finish
                ' Do other processing while waiting for 1 second to elapse
                totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Loop
            'observar - 13 MINUTOS con 1 segundo en cada retardo
            byteIn(0) = SerialPort1.ReadByte
            byteIn(1) = SerialPort1.ReadByte
            byteIn(2) = SerialPort1.ReadByte
            byteIn(3) = SerialPort1.ReadByte
            byteIn(4) = SerialPort1.ReadByte
            byteIn(5) = SerialPort1.ReadByte
            byteIn(6) = SerialPort1.ReadByte
            tam = byteIn(6)


            'TextBox4.Text = byteIn(0)
            'TextBox5.Text = byteIn(1)
            'TextBox6.Text = byteIn(2)
            'TextBox7.Text = byteIn(3)
            'TextBox8.Text = byteIn(4)
            'TextBox9.Text = byteIn(5)
            'TextBox10.Text = byteIn(6)

            'TextBox11.Text = SerialPort1.ReadByte
            'TextBox12.Text = SerialPort1.ReadByte
            'TextBox13.Text = SerialPort1.ReadByte
            'TextBox14.Text = SerialPort1.ReadByte
            'TextBox15.Text = SerialPort1.ReadByte
            'TextBox16.Text = SerialPort1.ReadByte
            'TextBox17.Text = SerialPort1.ReadByte
            'TextBox18.Text = SerialPort1.ReadByte
            'TextBox19.Text = SerialPort1.ReadByte
            'TextBox20.Text = SerialPort1.ReadByte
            'TextBox21.Text = SerialPort1.ReadByte

            '
            'Hace una segunda petición para leer ahora el valor de la temperatura
            'SerialPort1.Write(byteOut, 0, 7) 'send message

            TextBox1.Text = ""
            'Lee los siguientes bytes correspondientes al mensaje recibido
            For i = 7 To tam + 7
                TextBox1.Text = TextBox1.Text + Chr(SerialPort1.ReadByte)
            Next
            'Guarda en un archivo la lectura de temperatura recibida
            My.Computer.FileSystem.WriteAllText(ubicacion2, TextBox22.Text & " " & fecha & " " & hora & " " & TextBox1.Text & " " & Microsoft.VisualBasic.DateAndTime.DateString & " " & Microsoft.VisualBasic.DateAndTime.TimeOfDay & vbCrLf, False)

            'Cierra el puerto utilizado para la comunicación bluetooth
            SerialPort1.Close()
            Label1.Text = "Disconnected"
            Refresh()
            ' Set end time for 1-second duration.
            start = Microsoft.VisualBasic.DateAndTime.Timer
            finish = start + 1
            totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Do While totaltime < finish
                ' Do other processing while waiting for 5 seconds to elapse.
                totaltime = Microsoft.VisualBasic.DateAndTime.Timer
            Loop
        End While
    End Sub
    End Sub
End Class

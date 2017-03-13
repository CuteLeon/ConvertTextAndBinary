Imports System.Text

Public Class Form1
    Dim EncodeTypes() As Encoding = {Encoding.ASCII, Encoding.UTF8, Encoding.Unicode}

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = vbNullString OrElse ComboBox1.SelectedIndex = -1 Then Exit Sub
        TextBox2.Text = ConvertTextToBianry(TextBox1.Text, EncodeTypes(ComboBox1.SelectedIndex))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text = vbNullString OrElse ComboBox1.SelectedIndex = -1 Then Exit Sub
        TextBox1.Text = ConvertBianryToText(TextBox2.Text, EncodeTypes(ComboBox1.SelectedIndex))
    End Sub

    Private Function ConvertTextToBianry(IniString As String, EncodeType As Encoding) As String
        Dim ResultString As String = vbNullString
        Dim BitList As BitArray = New BitArray(EncodeType.GetBytes(IniString))
        For Index As Integer = 0 To BitList.Length - 1 Step 8
            For IndexInside As SByte = 7 To 0 Step -1
                ResultString &= IIf(BitList.Item(Index + IndexInside), "1", "0")
            Next
            ResultString &= " "
        Next
        Return ResultString
    End Function

    Private Function ConvertBianryToText(IniString As String, EncodeType As Encoding) As String
        Dim ResultString As String = vbNullString
        Dim IString As String = IniString.Replace(" ", String.Empty)
        Dim BitList As BitArray = New BitArray(IString.Length, False)
        For Index As Integer = 0 To IString.Length - 8 Step 8
            For IndexInside As Byte = 0 To 7
                If IString.Chars(Index + 7 - IndexInside) = "1" Then BitList.Set(Index + IndexInside, True)
            Next
        Next
        Dim Bytes(BitList.Length / 8 - 1) As Byte
        BitList.CopyTo(Bytes, 0)
        ResultString = EncodeType.GetString(Bytes)
        Return ResultString
    End Function
End Class

Imports System.Data.SqlClient

Public Class Form1
    ' Variables used to connect to the DB
    Dim sqlconn As SqlConnection
    Dim sqlcmd As SqlCommand
    Dim sqlRd As SqlDataReader
    Dim sqlDt As New DataTable
    Dim DtA As New SqlDataAdapter
    Dim sqlQuery As String

    ' MSSQL server and database name
    Dim Server As String = "VENOMIZER"
    Dim Database As String = "LibraryDB"

    Private bitmap As Bitmap

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load event handler, updates the table of books
        updatetable()
    End Sub

    Private Sub updatetable()
        Try
            ' Establish connection to SQL Server and fetch all books data
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"
            sqlconn.Open()

            sqlcmd = New SqlCommand("SELECT * FROM Books", sqlconn)
            sqlRd = sqlcmd.ExecuteReader()

            ' Load data into DataTable and bind it to DataGridView
            sqlDt.Clear()
            sqlDt.Load(sqlRd)
            sqlRd.Close()
            sqlconn.Close()
            dgvBooks.DataSource = sqlDt

        Catch ex As Exception
            ' Display error message if there's an exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ' Ensure connection is closed after use
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
        End Try
    End Sub

    ' ADD button functionality
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            ' Add a new book entry to the database
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"
            sqlconn.Open()

            sqlQuery = "INSERT INTO Books (Title, Author, YearPublished, Genre) VALUES (@Title, @Author, @YearPublished, @Genre)"

            sqlcmd = New SqlCommand(sqlQuery, sqlconn)
            sqlcmd.Parameters.AddWithValue("@Title", txtTitle.Text)
            sqlcmd.Parameters.AddWithValue("@Author", txtAuthor.Text)
            sqlcmd.Parameters.AddWithValue("@YearPublished", txtYearPublished.Text)
            sqlcmd.Parameters.AddWithValue("@Genre", txtGenre.Text)
            sqlcmd.ExecuteNonQuery()
            sqlconn.Close()

        Catch ex As Exception
            ' Display error message if there's an exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ' Ensure connection is closed after use and update table
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
            updatetable()
        End Try
    End Sub

    ' UPDATE button functionality
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            ' Update book details based on Title
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"
            sqlconn.Open()

            sqlQuery = "UPDATE Books SET Author = @Author, Genre = @Genre, YearPublished = @YearPublished WHERE Title = @Title"

            sqlcmd = New SqlCommand(sqlQuery, sqlconn)
            sqlcmd.Parameters.AddWithValue("@Author", txtAuthor.Text)
            sqlcmd.Parameters.AddWithValue("@Genre", txtGenre.Text)
            sqlcmd.Parameters.AddWithValue("@YearPublished", Integer.Parse(txtYearPublished.Text))
            sqlcmd.Parameters.AddWithValue("@Title", txtTitle.Text)
            sqlcmd.ExecuteNonQuery()
            sqlconn.Close()

        Catch ex As Exception
            ' Display error message if there's an exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ' Ensure connection is closed after use and update table
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
            updatetable()
        End Try
    End Sub

    ' DELETE button functionality
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            ' Delete book based on Title
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"
            sqlconn.Open()

            sqlQuery = "DELETE FROM Books WHERE Title = @Title"

            sqlcmd = New SqlCommand(sqlQuery, sqlconn)
            sqlcmd.Parameters.AddWithValue("@Author", txtAuthor.Text)
            sqlcmd.Parameters.AddWithValue("@Genre", txtGenre.Text)
            sqlcmd.Parameters.AddWithValue("@YearPublished", Integer.Parse(txtYearPublished.Text))
            sqlcmd.Parameters.AddWithValue("@Title", txtTitle.Text)
            sqlcmd.ExecuteNonQuery()
            sqlconn.Close()

        Catch ex As Exception
            ' Display error message if there's an exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ' Ensure connection is closed after use and update table
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
            updatetable()
        End Try
    End Sub

    ' CLEAR button functionality
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            ' Clear all entries from Books table
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"
            sqlconn.Open()

            sqlQuery = "DELETE FROM Books"

            sqlcmd = New SqlCommand(sqlQuery, sqlconn)
            sqlcmd.ExecuteNonQuery()
            sqlconn.Close()

        Catch ex As Exception
            ' Display error message if there's an exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ' Ensure connection is closed after use and update table
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
            updatetable()
        End Try
    End Sub

    Private Sub lblYearPublished_Click(sender As Object, e As EventArgs) Handles lblYearPublished.Click
        ' This event handler is empty and not used in the current code
    End Sub
End Class

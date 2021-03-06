Private TSlibrary_array
Private labcap_array
Private band_array
'Github is very unintuitive

Sub EUTRAN_CA()
    'Get the needed testcase
    'Get needed frequeny bands
    'for this named sheet
    'In the future- automate range retreiver
    'Match test specification from input list
    'if row 1 has specification, get row 2 if test case (if first three letter are 3GPP) skip)
    Dim lastRow As Long
    lastRow = Cells.Find("*", SearchOrder:=xlByRows, SearchDirection:=xlPrevious).Row
    Dim rg As Range, value As String, testspecresult As String, test_spec() As Variant
    Dim column_val As Variant
    test_spec = Array("31.121", "31.124", "34.121", "34.122", "36.521-1", "36.521-3", "36.523-1", "37.571-1", "37.571-2")
    testspecresult = createlibrary(test_spec)
    Set rg = Range("A6:C6")
    column_val = rg.value
    Dim i As Long
    For i = LBound(column_val) To UBound(column_val)
        value = column_val(i, 1)
        result = specification(value, lastRow)
    Next i
    Debug.Print "Done"
    
End Sub

Function specification(value, lastRow)
    Dim test_spec As String, rg As Range, i As Long, stry As Long, val As String
    Debug.Print lastRow
    stry = 1 ' need to automate
    Set rg = Range("A9:A" & lastRow) 'need to automate
    'Set rg = Range("A9:A18")
    Dim column_val As Variant, row_value As Long
    val = 8
    row_count = 1
    column_val = rg.value
    Dim condition As Boolean, column_result_1 As String
    'condition = False
   
    If value = "Specification" Then
        For i = LBound(column_val) To UBound(column_val)
        'Debug.Print "Checkin"
            val = val + 1
            If relevantTC(val) Then
                'Debug.Print "Checkin2"
                
                column_result = column_val(i, 1)
                row_value = i
                column_result_1 = "3GPP TS " & column_result & " "
                If TSlibrary_array.Exists(column_result) Then
                    row_count = row_count + 1
                    'Debug.Print "Checkin3"
                    result = write2sheet(column_result_1, row_count, stry, condition)
                    result_1 = testcase(i, row_count, lastRow)
                    result_2 = workitem(i, row_count, lastRow)
                    result_3 = bearertotest(i, row_count, lastRow)
                    result_4 = techarea(i, row_count, lastRow)
                Else
                    Debug.Print "Error Happened Somewhere"
                End If
                
                
            End If
        Next i
    End If
        
End Function

Function testcase(row_value, row_count, lastRow)
    Dim test_spec As String, rg As Range, cg As Range, i As Long, stry As Long
    Dim condition As Boolean
    
    stry = 2 ' need to automate
    stry_1 = 3 ' may be fine
    Set rg = Range("B9:B" & lastRow) 'need to automate
    Set cg = Range("C9:C" & lastRow) 'need to automate
    Dim column_val As Variant
    column_val = rg.value
    column_val_1 = cg.value
    column_result = column_val(row_value, 1)
    column_result_1 = column_val_1(row_value, 1)
    If Left(column_result, 4) = "3GPP" Then
        Debug.Print "here"
        condition = True
    End If
    result = write2sheet(column_result, row_count, stry, condition)
    result_1 = write2sheet(column_result_1, row_count, stry_1, condition)

    'Debug.Print column_val(row_value, 1)
    'Debug.Print column_val_1(row_value, 1)
    'For i = LBound(column_val) To UBound(column_val)
    '    column_result = column_val(i, 1)
    '    If TSlibrary_array.Exists(column_result) Then
    '        result = write2sheet(column_result, i, stry)
                
    '    Else
    '        Debug.Print "Error Happened Somewhere"
    '    End If
                        
    'Next i
    
End Function

Function workitem(row_value, row_count, lastRow)
    Dim test_spec As String, rg As Range, cg As Range, i As Long, stry As Long
    Dim condition As Boolean
    
    stry = 4
    Set rg = Range("E9:E" & lastRow) 'need to automate
    Dim column_val As Variant
    column_val = rg.value
    column_result = column_val(row_value, 1)
    condition = False
    result = write2sheet(column_result, row_count, stry, condition)
    
End Function

Function bearertotest(row_value, row_count, lastRow)
    Dim test_spec As String, rg As Range, cg As Range, i As Long, stry As Long
    Dim condition As Boolean
    
    stry = 5
    Set rg = Range("G9:G" & lastRow) 'need to automate
    Dim column_val As Variant
    column_val = rg.value
    column_result = column_val(row_value, 1)
    condition = False
    result = write2sheet(column_result, row_count, stry, condition)
End Function

Function techarea(row_value, row_count, lastRow)
    Dim test_spec As String, rg As Range, cg As Range, i As Long, stry As Long
    Dim condition As Boolean
    
    stry = 6
    Set rg = Range("F9:F" & lastRow) 'need to automate
    Dim column_val As Variant
    column_val = rg.value
    column_result = column_val(row_value, 1)
    column_dis = "" & column_result
    condition = False
    result = write2sheet(column_dis, row_count, stry, condition)
End Function

Function bandconditions()
    Dim rg As Range, i As Long, resul As Boolean
    Dim rg1 As String, rg2 As String
    
    Set rg = Range("H" & value & ":CT" & value) 'freaking tricky, may never know why
    Dim column_val As Variant
    column_val = rg.value
    Count = 0
    count_fal = 0
    
    For i = LBound(column_val) To UBound(column_val)
        For j = LBound(column_val, 2) To UBound(column_val, 2)
            column_result = column_val(i, j)
            If column_result = "-" Then
                resul = False
                Count = Count + 1
            Else
                resul = True
                count_fal = count_fal + 1
                Exit For
            End If
            
        Next j
    Next i
    
    'Debug.Print Count
    relevantTC = resul
End Function

Function relevantTC(value)
    Dim rg As Range, i As Long, resul As Boolean
    Dim rg1 As String, rg2 As String
      
    
    Set rg = Range("H" & value & ":CT" & value) 'freaking tricky, may never know why
    Dim column_val As Variant
    column_val = rg.value
    Count = 0
    count_fal = 0
    
    For i = LBound(column_val) To UBound(column_val)
        For j = LBound(column_val, 2) To UBound(column_val, 2)
            column_result = column_val(i, j)
            If column_result = "-" Then
                resul = False
                Count = Count + 1
            Else
                resul = True
                count_fal = count_fal + 1
                Exit For
            End If
            
        Next j
    Next i
    
    'Debug.Print Count
    relevantTC = resul
End Function

Function createlibrary(test_spec() As Variant) As String
    Dim row_lib As Object, row_result As String
    Set TSlibrary_array = CreateObject("Scripting.Dictionary")
    
    Dim v As Long
    For v = LBound(test_spec) To UBound(test_spec)
        
        row_result = test_spec(v)
        'Debug.Print row_result
        If TSlibrary_array.Exists(row_result) Then
            
            Debug.Print "Error occured somewhere"
        Else
            TSlibrary_array.Add row_result, v
        End If
    Next v
    
    
    'Dim u As Long
    'For u = 0 To row_lib.Count - 1
    '    Debug.Print (row_lib.Items()(u) & "and" & row_lib.Keys()(u))
    'Next u
    
End Function

Function labcaplibrary()
    'ActiveWorkbook.Worksheets ("Band_library")
    'Debug.Print "point1"
    Dim lastRow As Long
    lastRow = Cells.Find("*", SearchOrder:=xlByRows, SearchDirection:=xlPrevious).Row
    
    Set rg = ActiveWorkbook.Worksheets("Lab_Capability").Range("A1:A" & lastRow)  'need to automate
    Set rg1 = ActiveWorkbook.Worksheets("Lab_Capability").Range("B1:B" & lastRow)  'need to automate
    Dim rgval As Variant, rgval_1 As Variant
        
    rgval = rg.value
    rgval_1 = rg1.value
   
    Dim row_lib As Object, row_result As String
    Set labcap_array = CreateObject("Scripting.Dictionary")
    
    Dim v As Long
    For v = LBound(rgval) To UBound(rgval)
        'Debug.Print "point2"
        row_key = rgval(v, 1)
        row_data = rgval_1(v, 1)
        'Debug.Print row_result
        If labcap_array.Exists(row_key) Then
            
            Debug.Print "Error occured somewhere"
        Else
            labcap_array_array.Add row_key, row_data
        End If
    Next v
    
    
    'Dim u As Long
    'For u = 0 To TSlibrary_array.Count - 1
    '   Debug.Print (TSlibrary_array.Items()(u) & "and" & TSlibrary_array.Keys()(u))
    'Next u
    
End Function

Function bandlibrary()
    'ActiveWorkbook.Worksheets ("Band_library")
    'Debug.Print "point1"
    Dim lastRow As Long
    lastRow = Cells.Find("*", SearchOrder:=xlByRows, SearchDirection:=xlPrevious).Row
    
    Set rg = ActiveWorkbook.Worksheets("Band_library").Range("A1:A" & lastRow)  'need to automate
    Set rg1 = ActiveWorkbook.Worksheets("Band_library").Range("B1:B" & lastRow)  'need to automate
    Dim rgval As Variant, rgval_1 As Variant
        
    rgval = rg.value
    rgval_1 = rg1.value
   
    Dim row_lib As Object, row_result As String
    Set band_array = CreateObject("Scripting.Dictionary")
    
    Dim v As Long
    For v = LBound(rgval) To UBound(rgval)
        'Debug.Print "point2"
        row_key = rgval(v, 1)
        row_data = rgval_1(v, 1)
        'Debug.Print row_result
        If band_array.Exists(row_key) Then
            
            Debug.Print "Error occured somewhere"
        Else
            band_array.Add row_key, row_data
        End If
    Next v
    
    
    'Dim u As Long
    'For u = 0 To TSlibrary_array.Count - 1
    '   Debug.Print (TSlibrary_array.Items()(u) & "and" & TSlibrary_array.Keys()(u))
    'Next u
    
End Function

Function write2sheet(ByVal str1 As String, ByVal strx As Long, ByVal stry As Long, condition)
    Dim myWorksheet As Worksheet, sh As Worksheet
    Dim x As Long, y As Long, k As Long
    
    x = strx
    y = stry
    Sheets("Summary").Cells(x, y).NumberFormat = "@"
    Sheets("Summary").Cells(x, y).value = str1
    'Sheets("Summary").Cells(x, y).EntireRow.Interior.ColorIndex = 36
    'Debug.Print condition
    If condition Then
        Sheets("Summary").Cells(x, y).EntireRow.Interior.ColorIndex = 39
        Debug.Print condition
    End If
End Function

Function lookintocurrentsheet()
    'look into the current sheet and get last written row inside
End Function




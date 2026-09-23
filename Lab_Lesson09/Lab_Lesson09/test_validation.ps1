$form = @{
    Name = 'abc'
    CategoryId = '1'
    Price = '50000'
    SalePrice = '60000'
    Description = 'Sản phẩm test admin và die'
}

try {
    # Get request to obtain anti-forgery token or cookies if needed
    $session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
    $getResp = Invoke-WebRequest -Uri 'http://localhost:5090/Product/Create' -WebSession $session
    $tokenMatch = [regex]::Match($getResp.Content, 'name="__RequestVerificationToken" type="hidden" value="([^"]+)"')
    if ($tokenMatch.Success) {
        $form['__RequestVerificationToken'] = $tokenMatch.Groups[1].Value
    }

    $postResp = Invoke-WebRequest -Uri 'http://localhost:5090/Product/Create' -Method Post -Body $form -WebSession $session

    Write-Output "Status: $($postResp.StatusCode)"
    $matches = @(
        "Tên sản phẩm phải có từ 6 đến 150 ký tự",
        "Giá chuẩn phải nhỏ nhất là 100.000 VNĐ",
        "Giá khuyến mãi",
        "không được chứa các từ nhạy cảm",
        "Thuộc tính hình ảnh bắt buộc"
    )

    foreach ($m in $matches) {
        if ($postResp.Content -like "*$m*") {
            Write-Output "VALIDATION MATCHED: $m"
        } else {
            Write-Output "MISSING: $m"
        }
    }
} catch {
    Write-Output "Error: $_"
}

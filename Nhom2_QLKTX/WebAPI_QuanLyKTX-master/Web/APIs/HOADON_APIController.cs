@model Web.Models.HOADON
@using Web.Models;
@{
    ViewBag.Title = "Thêm mới";
    Layout = "~/Views/Shared/MainLayout.cshtml";
    var db = new Context();
    var list_phong = db.PHONGs.Where(p => p.tinhtrang == true).ToList();
    var list_hd = db.PHIEU_DIENNUOC.ToList();
}

<h2 class="hi"><i class="fas fa-file-invoice"></i> Thông tin</h2>

@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()

<div class="form-horizontal">
    <h4>Hóa đơn</h4>
    <hr />
    @Html.ValidationSummary(true, "", new { @class = "text-danger" })
    <div class="form-group">
        @Html.LabelFor(model => model.ngaytao, "Ngày tạo phiếu", htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.EditorFor(model => model.ngaytao, new { htmlAttributes = new { @class = "form-control", Value = DateTime.Now, @Readonly = "true" } })
            @Html.ValidationMessageFor(model => model.ngaytao, "", new { @class = "text-danger" })
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-10">
            <label class="control-label col-md-2" for="maphong">Phòng nhận hóa đơn</label>
            @if (list_phong != null && list_phong.ToList().Count > 0)
            {
                <select id="maphong" name="maphong">
                    <option value="">Hãy chọn</option>
                    @foreach (var i in list_phong.ToList())
                    {
                        <option value="@i.maphong" data-sogiuong="@i.tsogiuong" data-tenphong="@list_phong.FirstOrDefault(x => x.maphong == i.maphong).tenphong">
                            @list_phong.FirstOrDefault(x => x.maphong == i.maphong).tenphong (@i.tsogiuong)
                        </option>
                    }
                </select>}
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-10">
            <label class="control-label col-md-2" for="maphieudiennuoc">Phiếu điện nước</label>
            @if (list_hd != null && list_hd.Count > 0)
            {
                <select id="maphieudiennuoc" name="maphieudiennuoc">
                    <option value="">Hãy chọn</option>
                    @foreach (var i in list_hd)
                    {
                        var ma = i.maphieu + i.ngaytaophieu.Value.ToString("ddMyyyy");
                        <option value="@i.maphieu" data-tenphong="@i.PHONG.tenphong" e-value="@i.tongtien">
                            Mã phiếu: @ma -  @i.PHONG.tenphong -  @i.ngaytaophieu.Value.ToString("HH") giờ @i.ngaytaophieu.Value.ToString("mm") phút ngày @i.ngaytaophieu.Value.ToString("dd") tháng @i.ngaytaophieu.Value.ToString("MM")
                        </option>
                    }
                </select>}
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(model => model.tiendiennuoc, "Tiền điện - nước", htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.EditorFor(model => model.tiendiennuoc, new { htmlAttributes = new { @class = "form-control", @Readonly = "true" } })
            @Html.ValidationMessageFor(model => model.tiendiennuoc, "", new { @class = "text-danger" })
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(model => model.tienphong, "Tiền phòng", htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.EditorFor(model => model.tienphong, new { htmlAttributes = new { @class = "form-control", @id = "tienphong", @readonly = "readonly" } })
            @Html.ValidationMessageFor(model => model.tienphong, "", new { @class = "text-danger" })
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(model => model.tongtien, "Tổng tiền", htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.EditorFor(model => model.tongtien, new { htmlAttributes = new { @class = "form-control", @Readonly = "true" } })
            @Html.ValidationMessageFor(model => model.tongtien, "", new { @class = "text-danger" })
        </div>
    </div>

    <div class="form-group">
        <label for="tinhtrang">Tình trạng</label>
        <select id="tinhtrang" name="tinhtrang" class="form-control">
            <option value="Chưa thanh toán">Chưa thanh toán</option>
            <option value="Đã thanh toán">Đã thanh toán</option>
        </select>
    </div>


    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" value="Create" class="btn btn-default" />
        </div>
    </div>
</div>
}

<div>
    <i class="fa-regular fa-square-caret-left"></i> @Html.ActionLink("Quay lại", "Index")
</div>

<script>
    document.addEventListener('DOMContentLoaded', function () {
        const maphongSelect = document.getElementById('maphong');
        const maphieudiennuocSelect = document.getElementById('maphieudiennuoc');

        function validateSelection() {
            const selectedMaphong = maphongSelect.options[maphongSelect.selectedIndex];
            const selectedMaphieudiennuoc = maphieudiennuocSelect.options[maphieudiennuocSelect.selectedIndex];

            const maphongTenphong = selectedMaphong ? selectedMaphong.getAttribute('data-tenphong') : null;
            const phieudiennuocTenphong = selectedMaphieudiennuoc ? selectedMaphieudiennuoc.getAttribute('data-tenphong') : null;

            if (maphongTenphong && phieudiennuocTenphong && maphongTenphong !== phieudiennuocTenphong) {
                alert('Chọn phiếu điện nước hoặc phòng nhận hóa đơn không hợp lệ');
                maphongSelect.selectedIndex = 0; // Reset to "Hãy chọn"
                maphieudiennuocSelect.selectedIndex = 0; // Reset to "Hãy chọn"
            }
        }

        maphongSelect.addEventListener('change', validateSelection);
        maphieudiennuocSelect.addEventListener('change', validateSelection);
    });

    document.getElementById('maphong').addEventListener('change', function () {
        var sogiuong = parseInt(this.options[this.selectedIndex].getAttribute('data-sogiuong'));
        var tienphong = 0;

        if (sogiuong === 8) {
            tienphong = 500000; // Giá trị cố định cho 8 giường
        } else if (sogiuong === 4) {
            tienphong = 1000000; // Giá trị cố định cho 4 giường
        } else if (sogiuong === 2) {
            tienphong = 1800000; // Giá trị cố định cho 2 giường
        }

        document.getElementById('tienphong').value = tienphong;
        TinhTien(); // Gọi hàm tính tổng tiền
    });

    $(document).ready(function () {
        var TinhTien = function () {
            var Tong = $("#tongtien");
            var d = Number($("#tiendiennuoc").val());
            var p = Number($("#tienphong").val());
            console.log('dien', d, 'phong', p);
            Tong.val(d + p);
        }

        $("#maphieudiennuoc").change(function () {
            var _vl = $("#maphieudiennuoc option:selected").attr('e-value');
            console.log(_vl);
            $("#tiendiennuoc").val(_vl);
            TinhTien();
        });

        $("#tienphong").change(function () {
            if ($(this).val() < 0 || isNaN($(this).val())) {
                alert("Giá trị không hợp lệ");
                $(this).val('0');
            }
            TinhTien();
        });
    });
</script>

using System;
using System.Collections.Generic;

class TiemBanhApp
{
    // Khai báo tên bánh và giá tương ứng
    static string[] dsBanh = { "Bánh mì", "Bánh kem", "Bánh su", "Bánh tart", "Bánh bông lan" };
    static double[] giaBanh = { 10000, 50000, 15000, 30000, 20000 };

    // Thống kê đơn hàng
    static int tongSoDon = 0;
    static int soDonLon = 0;
    static int soDonThuong = 0;
    static double tongDoanhThu = 0;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Lời chào + hướng dẫn
        Console.WriteLine("🎂 CHÀO MỪNG ĐẾN VỚI HỆ THỐNG QUẢN LÝ ĐƠN HÀNG TIỆM BÁNH 🎂");
        Console.WriteLine("Nhập tên bánh bạn muốn mua và số lượng. Gõ 'exit' để kết thúc.");
        Console.WriteLine("\n📋 Danh sách bánh hiện có:");
        for (int i = 0; i < dsBanh.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {dsBanh[i]} - {giaBanh[i]} VND");
        }

        while (true)
        {
            Console.Write("\nNhập tên bánh (hoặc 'exit'): ");
            string tenBanh = Console.ReadLine();

            if (tenBanh.Trim().ToLower() == "exit")
                break;

            // Kiểm tra tên bánh có hợp lệ
            int viTriBanh = TimViTriBanh(tenBanh);
            if (viTriBanh == -1)
            {
                Console.WriteLine("❌ Loại bánh không tồn tại. Vui lòng thử lại.");
                continue;
            }

            Console.Write("Nhập số lượng: ");
            if (!int.TryParse(Console.ReadLine(), out int soLuong) || soLuong <= 0)
            {
                Console.WriteLine("❌ Số lượng không hợp lệ. Vui lòng nhập số nguyên dương.");
                continue;
            }

            double tongTien = TinhTien(dsBanh[viTriBanh], soLuong); // dùng overload 1
            string loaiDon = XepLoaiDon(tongTien);
            HienThiThongTin(tenBanh, soLuong, tongTien, loaiDon);

            // Cập nhật thống kê
            tongSoDon++;
            tongDoanhThu += tongTien;
            if (loaiDon == "Đơn lớn") soDonLon++;
            else soDonThuong++;
        }

        // Hiển thị thống kê
        Console.WriteLine("\n📊 THỐNG KÊ CUỐI NGÀY:");
        Console.WriteLine($"Tổng số đơn hàng: {tongSoDon}");
        Console.WriteLine($"Tổng doanh thu: {tongDoanhThu:N0} VND");
        Console.WriteLine($"Số đơn lớn: {soDonLon}");
        Console.WriteLine($"Số đơn thường: {soDonThuong}");
        Console.WriteLine("\n💤 Kết thúc chương trình. Cảm ơn bạn đã sử dụng!");

        Console.ReadKey();
    }

    // 🔍 Tìm vị trí bánh trong mảng
    static int TimViTriBanh(string tenBanh)
    {
        for (int i = 0; i < dsBanh.Length; i++)
        {
            if (dsBanh[i].ToLower() == tenBanh.Trim().ToLower())
                return i;
        }
        return -1;
    }

    // 💰 Tính tiền theo tên bánh (Method Overloading #1)
    static double TinhTien(string tenBanh, int soLuong)
    {
        int viTri = TimViTriBanh(tenBanh);
        if (viTri != -1)
        {
            return giaBanh[viTri] * soLuong;
        }
        return 0;
    }

    // 💰 Tính tiền theo giá trực tiếp (Method Overloading #2)
    static double TinhTien(int gia, int soLuong)
    {
        return gia * soLuong;
    }

    // 📦 Phân loại đơn hàng
    static string XepLoaiDon(double tongTien)
    {
        return tongTien > 100000 ? "Đơn lớn" : "Đơn thường";
    }

    // 📄 Hiển thị thông tin đơn hàng
    static void HienThiThongTin(string tenBanh, int soLuong, double tongTien, string loaiDon)
    {
        Console.WriteLine("\n🧾 CHI TIẾT ĐƠN HÀNG:");
        Console.WriteLine($"Bánh: {tenBanh}");
        Console.WriteLine($"Số lượng: {soLuong}");
        Console.WriteLine($"Tổng tiền: {tongTien:N0} VND");
        Console.WriteLine($"Loại đơn: {loaiDon}");
    }
}

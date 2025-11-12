function jalaliToGregorian(jy, jm, jd) {
    jy += 1595;
    var days = -355668 + (365 * jy) + (Math.floor(jy / 33) * 8) + Math.floor(((jy % 33) + 3) / 4) + jd + ((jm < 7) ? (jm - 1) * 31 : ((jm - 7) * 30) + 186);
    var gy = 400 * Math.floor(days / 146097);
    days %= 146097;
    if (days > 36524) {
        gy += 100 * Math.floor(--days / 36524);
        days %= 36524;
        if (days >= 365) days++;
    }
    gy += 4 * Math.floor(days / 1461);
    days %= 1461;
    if (days > 365) {
        gy += Math.floor((days - 1) / 365);
        days = (days - 1) % 365;
    }
    var gd = days + 1;
    var sal_a = [0, 31, (gy % 4 === 0 && gy % 100 !== 0) || (gy % 400 === 0) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    for (var gm = 0; gm < 13; gm++) {
        var v = sal_a[gm];
        if (gd <= v) break;
        gd -= v;
    }
    return [gy, gm, gd];
}

function gregorianToJalali(gy, gm, gd) {
    var g_d_m = [0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334];
    var gy2 = (gm > 2) ? (gy + 1) : gy;
    var days = 355666 + (365 * gy) + Math.floor((gy2 + 3) / 4) - Math.floor((gy2 + 99) / 100) + Math.floor((gy2 + 399) / 400) + gd + g_d_m[gm - 1];
    var jy = -1595 + (33 * Math.floor(days / 12053));
    days %= 12053;
    jy += 4 * Math.floor(days / 1461);
    days %= 1461;
    if (days > 365) {
        jy += Math.floor((days - 1) / 365);
        days = (days - 1) % 365;
    }
    var jm, jd;
    if (days < 186) {
        jm = 1 + Math.floor(days / 31);
        jd = 1 + (days % 31);
    } else {
        jm = 7 + Math.floor((days - 186) / 30);
        jd = 1 + ((days - 186) % 30);
    }
    return [jy, jm, jd];
}

/* ===== تقویم اصلی ===== */
document.addEventListener("DOMContentLoaded", function () {
    const calendar = document.getElementById("calendar");
    const input = document.getElementById("DueDate");

    const weekDays = ["ش", "ی", "د", "س", "چ", "پ", "ج"];

    const today = new Date();
    let [jy, jm, jd] = gregorianToJalali(today.getFullYear(), today.getMonth() + 1, today.getDate());

    // 🟢 اگر مقدار مدل (میلادی) وجود دارد → به شمسی تبدیل و در input نمایش داده شود
    if (input.value && input.value.trim() !== "") {
        try {
            const gDate = new Date(input.value);
            if (!isNaN(gDate)) {
                [jy, jm, jd] = gregorianToJalali(gDate.getFullYear(), gDate.getMonth() + 1, gDate.getDate());
                input.value = `${jy}/${String(jm).padStart(2, '0')}/${String(jd).padStart(2, '0')}`;
            }
        } catch (e) {
            console.error("خطا در تبدیل تاریخ مدل:", e);
        }
    }

    // ===== تابع محاسبه تعداد روز در ماه شمسی =====
    function daysInJalaliMonth(y, m) {
        if (m <= 6) return 31;
        if (m <= 11) return 30;
        return ((y % 33) === 1 || (y % 33) === 5 || (y % 33) === 9 || (y % 33) === 13 ||
            (y % 33) === 17 || (y % 33) === 22 || (y % 33) === 26 || (y % 33) === 30) ? 30 : 29;
    }

    // ===== تابع رندر تقویم =====
    function renderCalendar() {
        let html = `
            <div class="header">
                <button class="nav-btn prev" id="prevMonth">&#8249;</button>
                <div class="year-month">${jy} / ${jm}</div>
                <button class="nav-btn next" id="nextMonth">&#8250;</button>
            </div>
            <div class="header">
                <span class="year-select" id="prevYear">سال قبل</span>
                <span class="year-select" id="nextYear">سال بعد</span>
            </div>
            <table>
                <thead><tr>${weekDays.map(d => `<th>${d}</th>`).join("")}</tr></thead>
                <tbody>
        `;

        const gOfFirst = jalaliToGregorian(jy, jm, 1);
        const firstDay = new Date(gOfFirst[0], gOfFirst[1] - 1, gOfFirst[2]).getDay();
        const totalDays = daysInJalaliMonth(jy, jm);

        let day = 1;
        for (let r = 0; r < 6; r++) {
            html += "<tr>";
            for (let c = 0; c < 7; c++) {
                if (r === 0 && c < firstDay) {
                    html += "<td></td>";
                } else if (day <= totalDays) {
                    let isSelected = (day === jd);
                    html += `<td class="day ${isSelected ? "selected" : ""}" data-d="${day}">${day}</td>`;
                    day++;
                } else {
                    html += "<td></td>";
                }
            }
            html += "</tr>";
        }
        html += "</tbody></table>";
        calendar.innerHTML = html;

        // کنترل ماه و سال
        document.getElementById("prevMonth").onclick = function (e) {
            e.stopPropagation();
            jm--;
            if (jm < 1) { jm = 12; jy--; }
            renderCalendar();
        };
        document.getElementById("nextMonth").onclick = function (e) {
            e.stopPropagation();
            jm++;
            if (jm > 12) { jm = 1; jy++; }
            renderCalendar();
        };
        document.getElementById("prevYear").onclick = function (e) {
            e.stopPropagation();
            jy--;
            renderCalendar();
        };
        document.getElementById("nextYear").onclick = function (e) {
            e.stopPropagation();
            jy++;
            renderCalendar();
        };

        // انتخاب روز
        document.querySelectorAll("#calendar .day").forEach(d => {
            d.addEventListener("click", function () {
                const day = parseInt(this.dataset.d);
                const g = jalaliToGregorian(jy, jm, day);
                const gDateStr = `${g[0]}/${String(g[1]).padStart(2, '0')}/${String(g[2]).padStart(2, '0')}`;
                input.value = `${jy}/${String(jm).padStart(2, '0')}/${String(day).padStart(2, '0')}`;
                calendar.style.display = "none";
            });
        });
    }

    // باز شدن تقویم
    input.addEventListener("focus", function () {
        const rect = input.getBoundingClientRect();
        calendar.style.top = rect.bottom + window.scrollY + "px";
        calendar.style.left = rect.left + "px";
        calendar.style.display = "block";
        renderCalendar();
    });

    // بستن با کلیک بیرون
    document.addEventListener("click", function (e) {
        if (!calendar.contains(e.target) && e.target !== input) {
            calendar.style.display = "none";
        }
    });

    // 🟣 هنگام ارسال فرم → تاریخ شمسی را قبل از ارسال به میلادی تبدیل کن
    const form = input.closest("form");
    if (form) {
        form.addEventListener("submit", function () {
            const val = input.value.trim();
            if (val.match(/^\d{4}\/\d{2}\/\d{2}$/)) { // مثل 1404/08/20
                const [jy, jm, jd] = val.split("/").map(Number);
                const [gy, gm, gd] = jalaliToGregorian(jy, jm, jd);
                input.value = `${gy}-${String(gm).padStart(2, '0')}-${String(gd).padStart(2, '0')}`;
            }
        });
    }
});




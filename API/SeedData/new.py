import json
import os

def find_missing_trip_stop_ids():
    """
    يقوم بتحميل بيانات رحلات التوقف وأسعار قطاعات الرحلات،
    ويقارن بينها لإيجاد TripStopID الموجودة في ملف الأسعار
    وليست موجودة كمعرف توقف في ملف TripStop.
    """
    trip_stop_file = 'TripStop.json'
    segment_price_file = 'TripSegmentPrice.json'

    # 1. التحقق من وجود الملفات
    if not os.path.exists(trip_stop_file):
        print(f"خطأ: لم يتم العثور على الملف {trip_stop_file}")
        return
    if not os.path.exists(segment_price_file):
        print(f"خطأ: لم يتم العثور على الملف {segment_price_file}")
        return

    # 2. تحميل البيانات
    try:
        # **التعديل هنا:** استخدام 'utf-8-sig' للتعامل مع Byte Order Mark (BOM)
        with open(trip_stop_file, 'r', encoding='utf-8-sig') as f:
            trip_stops_data = json.load(f)

        # **التعديل هنا:** استخدام 'utf-8-sig' للتعامل مع Byte Order Mark (BOM)
        with open(segment_price_file, 'r', encoding='utf-8-sig') as f:
            segment_prices_data = json.load(f)
    except json.JSONDecodeError as e:
        print(f"خطأ في تحليل ملف JSON: {e}")
        return
    except Exception as e:
        print(f"حدث خطأ أثناء قراءة الملفات: {e}")
        return

    # 3. استخلاص كل معرفات TripStop الموجودة في ملف TripStop.json
    # نستخدم مجموعة (set) لسرعة البحث وتفادي التكرار
    known_stop_ids = set()
    for stop in trip_stops_data:
        # نفترض أن مفتاح المعرف في ملف TripStop هو 'TripStopID'
        if 'TripStopID' in stop and stop['TripStopID'] is not None:
            # يفضل الاحتفاظ بنوع البيانات الأصلي (عادةً رقم صحيح)
            known_stop_ids.add(stop['TripStopID'])

    # 4. استخلاص كل معرفات التوقف (StartStopID و EndStopID) من ملف TripSegmentPrice.json
    segment_stop_ids = set()
    for segment in segment_prices_data:
        # استخلاص StartStopID
        if 'StartStopID' in segment and segment['StartStopID'] is not None:
            segment_stop_ids.add(segment['StartStopID'])

        # استخلاص EndStopID
        if 'EndStopID' in segment and segment['EndStopID'] is not None:
            segment_stop_ids.add(segment['EndStopID'])

    # 5. إيجاد المعرفات الموجودة في الأسعار وليست موجودة في التوقفات
    # نستخدم عملية الفرق بين المجموعات (set difference)
    missing_stop_ids = segment_stop_ids - known_stop_ids

    # 6. عرض النتائج
    if missing_stop_ids:
        # نقوم بتحويل المجموعة إلى قائمة ثم فرزها للعرض المنظم
        sorted_missing_ids = sorted(list(missing_stop_ids))
        print("-" * 50)
        print("TripStopID الموجودة في TripSegmentPrice وليست في TripStop:")
        print(f"العدد الإجمالي للمعرفات المفقودة: {len(sorted_missing_ids)}")
        print("-" * 50)
        # طباعة كل معرف مفقود
        for missing_id in sorted_missing_ids:
            print(f"  - {missing_id}")
        print("-" * 50)
    else:
        print("-" * 50)
        print("لا توجد TripStopID مفقودة. جميع المعرفات في ملف الأسعار موجودة في ملف التوقفات.")
        print("-" * 50)

# تنفيذ الدالة
if __name__ == "__main__":
    find_missing_trip_stop_ids()
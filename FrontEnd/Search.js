document.addEventListener('DOMContentLoaded', () => {
    // 1. تحديد العناصر الرئيسية في الصفحة
    const resultsSection = document.getElementById('results-section');
    const titleElement = document.getElementById('results-title'); 
    // تحديد عناصر موديل اختيار الدرجة
    const modal = document.getElementById('classSelectionModal');
    const classOptionsContainer = document.getElementById('classOptionsContainer');
    
    // تحديد عناصر موديل عرض المسار الجديد
    const tripMapModal = document.getElementById('tripMapModal');
    const tripMapContainer = document.getElementById('tripMapContainer');
    const tripMapTitle = document.getElementById('tripMapTitle');

    // كائن لتخزين معرفات فترات التتبع (Intervals) لكل رحلة، لإيقافها لاحقاً.
    const trackingIntervals = {};

    // إزالة المحتوى الثابت لعرض المحتوى الديناميكي
    resultsSection.innerHTML = '';

    // 2. قراءة البيانات المخزنة من localStorage
    const resultsJson = localStorage.getItem('searchResults');
    const fromStationName = localStorage.getItem('searchFromStationName') || 'محطة الانطلاق';
    // تصحيح: يجب استخدام localStorage لجلب اسم محطة الوصول
    const toStationName = localStorage.getItem('searchToStationName') || 'محطة الوصول';
    
    let trips = [];
    if (resultsJson) {
        try {
            trips = JSON.parse(resultsJson);
        } catch (e) {
            console.error('Error parsing search results:', e);
        }
    }

    // 3. تحديث العنوان بناءً على محطات البحث
    if (titleElement) {
        // العودة لاستخدام فئة اللون الذهبي
        titleElement.innerHTML = `الرحلات المتاحة من <span class="text-gold">${fromStationName}</span> إلى <span class="text-gold">${toStationName}</span>`;
    }

    // 4. عرض النتائج
    if (trips && trips.length > 0) {
        // إنشاء عنصر حاوية للرحلات
        const tripsContainer = document.createElement('div');
        tripsContainer.className = 'trips-container'; 

        trips.forEach(trip => {
            
            // **تعديل: جمع جميع الأسعار المتاحة في قالب HTML لعرضها**
            const allPricesHtml = trip.segmentPrices && trip.segmentPrices.length > 0 
                ? trip.segmentPrices.map(p => `
                    <div class="price-detail">
                        <span class="price-value">${p.price} ج.م</span>
                        <span class="price-class">${p.classNameAR}</span>
                    </div>
                  `).join('')
                : '<span class="price-value">غير متاح</span>';

            // تحديد أقل سعر لاستخدامه في زر الحجز (إذا لزم الأمر)
            const minPrice = trip.segmentPrices.length > 0
                ? Math.min(...trip.segmentPrices.map(p => p.price))
                : 'N/A';
            
            // جمع جميع الدرجات المتاحة
            const availableClasses = trip.availableClasses
                .map(cls => cls.classNameAR)
                .join(', ');

            // حساب عدد الوقفات (المحطات الوسيطة فقط، باستثناء الانطلاق والوصول)
            const stopsInTrip = trip.stops.filter(s => s.stationNameAR !== fromStationName && s.stationNameAR !== toStationName);
            const stopCount = stopsInTrip.length;

            // البحث عن محطة الانطلاق والوصول النهائية للحصول على أوقات المغادرة والوصول الفعلية
            const departureStop = trip.stops.find(s => s.stationNameAR === fromStationName);
            const arrivalStop = trip.stops.find(s => s.stationNameAR === toStationName);
            
            // نحتاج الوقت الأصلي بصيغة HH:MM:SS لحساب المدة
            const departureTimeStr = departureStop?.departureTime;
            const arrivalTimeStr = arrivalStop?.arrivalTime;
            
            // تنسيق الوقت للعرض
            const departureTime = departureTimeStr ? formatTime(departureTimeStr) : 'N/A';
            const arrivalTime = arrivalTimeStr ? formatTime(arrivalTimeStr) : 'N/A';
            
            // حساب المدة باستخدام الدالة المعدلة
            const duration = calculateDuration(departureTimeStr, arrivalTimeStr);

            // إزالة الشرطة من اسم القطار
            const cleanTrainName = trip.trainName.replace('-', '').trim();


            // إنشاء بطاقة التذكرة (div.ticket)
            const ticketDiv = document.createElement('div');
            ticketDiv.className = 'ticket';
            ticketDiv.innerHTML = `
                <div class="cut left"></div>
                <div class="cut right"></div>

                <div class="ticket-content">
                    <div class="info">
                        <h3>قطار ${cleanTrainName}</h3>
                        
                        <div class="trip-timeline">
                            <div class="timeline-path">
                                <span class="timeline-bar"></span>
                                <span class="duration-badge">${duration}</span>
                                <div class="timeline-point">
                                    <div class="timeline-station">${fromStationName}</div>
                                    <div class="timeline-time">${departureTime}</div>
                                </div>
                                <div class="timeline-point">
                                    <div class="timeline-station">${toStationName}</div>
                                    <div class="timeline-time">${arrivalTime}</div>
                                </div>
                            </div>
                        </div>

                        <div class="info-details">
                            <div class="info-detail-item">
                                <span>الدرجات المتاحة:</span>
                                <strong>${availableClasses}</strong>
                            </div>
                            <div class="info-detail-item">
                                <span>المدة الإجمالية:</span>
                                <strong>${duration}</strong>
                            </div>
                            <div class="info-detail-item">
                                <span>عدد الوقفات:</span>
                                <strong>${stopCount}</strong>
                            </div>
                            <div class="info-detail-item">
                                <strong class="stops-link" data-trip-id="${trip.trip_ID}">عرض المسار الكامل</strong>
                            </div>
                        </div>
                    </div>

                    <div class="price">
                        ${allPricesHtml}
                        <button class="btn-reserve select-trip" 
                                data-trip-id="${trip.trip_ID}" 
                                data-train-name="${cleanTrainName}"
                                onclick="showClassSelectionModal('${trip.trip_ID}', '${cleanTrainName}')">
                            احجز الآن
                        </button>
                    </div>
                </div>
            `;
            tripsContainer.appendChild(ticketDiv);
        });

        resultsSection.appendChild(tripsContainer);

        // 5. إضافة مستمعي الأحداث لزر عرض الوقفات
        document.querySelectorAll('.stops-link').forEach(link => {
            link.addEventListener('click', (event) => {
                const tripId = event.target.dataset.tripId;
                const tripData = trips.find(t => t.trip_ID == tripId);
                
                if (tripData) {
                    showTripMapModal(tripData);
                } else {
                    console.error("Trip data not found for ID:", tripId);
                }
            });
        });

    } else {
        // 6. عرض رسالة عدم وجود رحلات
        resultsSection.innerHTML = `
            <div class="no-results-card">
                <h3>عفواً، لا توجد رحلات متاحة حالياً.</h3>
                <p>يرجى تجربة مسار أو تاريخ آخر.</p>
                <a href="Book.html" class="btn-reserve">عودة للبحث</a>
            </div>
        `;
    }


    // ==========================================================
    // ** دوال التتبع الجديدة والمُعدلة (Tracking Functions) **
    // ==========================================================
    
    /**
     * حساب الثواني منذ منتصف الليل للوقت المحدد.
     * @param {string} timeStr - الوقت بصيغة "HH:MM:SS"
     * @returns {number} الثواني منذ منتصف الليل.
     */
    function timeToSeconds(timeStr) {
        if (!timeStr) return 0;
        try {
            const parts = timeStr.split(':').map(Number);
            if (parts.length < 2) return 0; 
            const h = parts[0];
            const m = parts[1];
            const s = parts.length > 2 ? parts[2] : 0;
            return h * 3600 + m * 60 + s;
        } catch (e) {
            return 0;
        }
    }

    /**
     * تحديث موضع القطار بناءً على الوقت الحالي.
     * @param {Object} tripData - بيانات الرحلة.
     */
    function updateTrainPosition(tripData) {
        const trainIcon = document.getElementById(`train-icon-${tripData.trip_ID}`);
        const trackingStatusElement = document.getElementById(`tracking-status-${tripData.trip_ID}`);
        const stopsListElement = document.getElementById(`stops-list-${tripData.trip_ID}`);

        
        if (!trainIcon || !trackingStatusElement || !stopsListElement) {
            stopTracking(tripData.trip_ID);
            return;
        }
        
        const allStops = tripData.stops;
        
        // 1. حساب مدة الرحلة الكلية والوقت الحالي بالنسبة لها
        const firstStop = allStops[0];
        const lastStop = allStops[allStops.length - 1];
        
        const tripStartTime = timeToSeconds(firstStop.departureTime);
        let tripEndTime = timeToSeconds(lastStop.arrivalTime);
        let currentTimeSeconds = timeToSeconds(new Date().toLocaleTimeString('en-US', { hour12: false }));
        
        // معالجة الرحلات التي تمتد ليومين
        if (tripEndTime < tripStartTime) {
            tripEndTime += 24 * 3600; 
            if (currentTimeSeconds < tripStartTime) {
                 currentTimeSeconds += 24 * 3600; 
            }
        }
        
        const totalDurationSeconds = tripEndTime - tripStartTime;
        
        let positionPercent = 0;
        let statusMessage = `القطار في حالة استعداد للمغادرة من ${firstStop.stationNameAR}.`;

        if (totalDurationSeconds <= 0) {
            positionPercent = 0;
        } else if (currentTimeSeconds >= tripEndTime) {
            // القطار وصل (أو تجاوز وقت الوصول)
            positionPercent = 100;
            statusMessage = `وصل القطار إلى وجهته النهائية: ${lastStop.stationNameAR}.`;
            
        } else if (currentTimeSeconds >= tripStartTime && currentTimeSeconds < tripEndTime) {
            // القطار في حالة حركة
            
            let currentSegmentStart = tripStartTime;
            let currentSegmentEnd = tripEndTime;
            let foundSegment = false;
            
            for (let i = 0; i < allStops.length; i++) {
                const stop = allStops[i];
                let arrivalTimeSec = timeToSeconds(stop.arrivalTime);
                let departureTimeSec = timeToSeconds(stop.departureTime);

                if (arrivalTimeSec < tripStartTime) arrivalTimeSec += 24 * 3600;
                if (departureTimeSec < tripStartTime) departureTimeSec += 24 * 3600;

                if (currentTimeSeconds >= arrivalTimeSec && currentTimeSeconds <= departureTimeSec) {
                    // القطار متوقف في المحطة الحالية
                    statusMessage = `القطار متوقف حالياً في محطة: ${stop.stationNameAR}.`;
                    
                    // تحديد موضع النقطة على الشريط
                    positionPercent = ((arrivalTimeSec - tripStartTime) / totalDurationSeconds) * 100;
                    foundSegment = true;
                    break;
                }
                
                if (i < allStops.length - 1) {
                    const nextStop = allStops[i + 1];
                    let nextArrivalSec = timeToSeconds(nextStop.arrivalTime);
                    if (nextArrivalSec < tripStartTime) nextArrivalSec += 24 * 3600;
                    
                    if (currentTimeSeconds >= departureTimeSec && currentTimeSeconds < nextArrivalSec) {
                        // القطار يتحرك بين المحطة i والمحطة i+1
                        currentSegmentStart = departureTimeSec;
                        currentSegmentEnd = nextArrivalSec;
                        
                        const segmentDuration = currentSegmentEnd - currentSegmentStart;
                        const elapsedInSegment = currentTimeSeconds - currentSegmentStart;
                        const segmentPercentage = elapsedInSegment / segmentDuration;

                        const startTotalPercentage = ((currentSegmentStart - tripStartTime) / totalDurationSeconds) * 100;
                        const endTotalPercentage = ((currentSegmentEnd - tripStartTime) / totalDurationSeconds) * 100;
                        
                        positionPercent = startTotalPercentage + (segmentPercentage * (endTotalPercentage - startTotalPercentage));
                        
                        statusMessage = `القطار في طريقه من ${stop.stationNameAR} إلى ${nextStop.stationNameAR}.`;
                        foundSegment = true;
                        break;
                    }
                }
            }

            if (!foundSegment && totalDurationSeconds > 0) {
                 positionPercent = ((currentTimeSeconds - tripStartTime) / totalDurationSeconds) * 100;
                 statusMessage = `القطار يتحرك بين المحطات.`;
            }
        } else {
             // الرحلة لم تبدأ بعد
             positionPercent = 0;
        }
        
        positionPercent = Math.min(100, Math.max(0, positionPercent));

        // 2. تطبيق الموضع العمودي على أيقونة القطار
        
        // الـ Timeline Wrapper هو الحاوية الرئيسية التي تحتوي على الشريط والقائمة
        const timelineWrapper = document.getElementById('timeline-wrapper');
        if (timelineWrapper) {
            
            // حساب موضع أيقونة القطار بالبكسل
            const listItems = Array.from(stopsListElement.querySelectorAll('li'));
            
            // الحصول على الارتفاع الكلي الفعلي للقائمة (للتمرير)
            const listHeight = stopsListElement.scrollHeight; 
            
            // تحديد النقاط المرجعية على الشريط
            let referenceItemIndex = -1;
            for (let i = 0; i < listItems.length; i++) {
                const stopItem = listItems[i];
                const stopName = stopItem.dataset.stationName;
                const stopData = allStops.find(s => s.stationNameAR === stopName);
                
                const stopTimeStr = (i === listItems.length - 1) ? stopData.arrivalTime : stopData.departureTime;
                let stopTimeSec = timeToSeconds(stopTimeStr);
                if (stopTimeSec < tripStartTime) stopTimeSec += 24 * 3600;
                
                const stopPercent = (stopTimeSec - tripStartTime) / totalDurationSeconds * 100;
                
                if (positionPercent >= stopPercent) {
                    referenceItemIndex = i;
                }
            }
            
            // 3. حساب موضع أيقونة القطار بالبكسل
            
            let referenceTop = listItems[0].offsetTop + (listItems[0].offsetHeight / 2); 
            let referenceTimePercent = 0;
            let nextTimePercent = 100;
            let nextTop = listItems[listItems.length - 1].offsetTop + (listItems[listItems.length - 1].offsetHeight / 2);
            
            if (referenceItemIndex !== -1) {
                const refItem = listItems[referenceItemIndex];
                referenceTop = refItem.offsetTop + (refItem.offsetHeight / 2);
                
                const refStopData = allStops.find(s => s.stationNameAR === refItem.dataset.stationName);
                const refTimeStr = (referenceItemIndex === listItems.length - 1) ? refStopData.arrivalTime : refStopData.departureTime;
                let refTimeSec = timeToSeconds(refTimeStr);
                if (refTimeSec < tripStartTime) refTimeSec += 24 * 3600;
                referenceTimePercent = (refTimeSec - tripStartTime) / totalDurationSeconds;

                if (referenceItemIndex < listItems.length - 1) {
                    const nextItem = listItems[referenceItemIndex + 1];
                    nextTop = nextItem.offsetTop + (nextItem.offsetHeight / 2);
                    
                    const nextStopData = allStops.find(s => s.stationNameAR === nextItem.dataset.stationName);
                    const nextTimeStr = nextStopData.arrivalTime;
                    let nextTimeSec = timeToSeconds(nextTimeStr);
                    if (nextTimeSec < tripStartTime) nextTimeSec += 24 * 3600;
                    nextTimePercent = (nextTimeSec - tripStartTime) / totalDurationSeconds;
                }
            }
            
            const totalTimePercent = (currentTimeSeconds - tripStartTime) / totalDurationSeconds;
            
            let segmentRatio = 0;
            if (nextTimePercent > referenceTimePercent) {
                segmentRatio = (totalTimePercent - referenceTimePercent) / (nextTimePercent - referenceTimePercent);
            }
            segmentRatio = Math.min(1, Math.max(0, segmentRatio)); 
            
            // حساب الموضع النهائي بالبكسل بالنسبة لأعلى القائمة
            const finalPositionPx = referenceTop + ((nextTop - referenceTop) * segmentRatio);
            
            // تطبيق الموضع على الأيقونة. (نطرح scroll-top لتبقى الأيقونة ثابتة بالنسبة للعرض المرئي)
            const trainIconWrapper = document.getElementById('detailed-timeline-wrapper');
            if (trainIconWrapper) {
                trainIcon.style.top = `${finalPositionPx - timelineWrapper.scrollTop - trainIconWrapper.offsetTop}px`;
            }

            // التمرير التلقائي لتركيز القطار في المنتصف (Scroll into view)
            if (referenceItemIndex !== -1 && referenceItemIndex < listItems.length) {
                listItems[referenceItemIndex].scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
        }

        // 4. تحديث رسالة التتبع وتمييز المحطة
        trackingStatusElement.textContent = statusMessage;
        highlightCurrentStop(tripData.trip_ID, statusMessage, allStops);
    }
    
    /**
     * تمييز المحطة الحالية في قائمة المحطات التفصيلية.
     * @param {string} tripId - معرف الرحلة.
     * @param {string} statusMessage - رسالة حالة التتبع.
     * @param {Array<Object>} allStops - قائمة المحطات.
     */
    function highlightCurrentStop(tripId, statusMessage, allStops) {
        const stopsListElement = document.getElementById(`stops-list-${tripId}`);
        if (!stopsListElement) return;

        stopsListElement.querySelectorAll('li').forEach(li => li.classList.remove('current-stop'));
        
        let currentStationName = null;

        // استخراج اسم المحطة الحالية من رسالة الحالة
        const matchStopped = statusMessage.match(/متوقف حالياً في محطة: (.+)\./);
        const matchArriving = statusMessage.match(/في طريقه من (.+) إلى (.+)\./);
        const matchReady = statusMessage.match(/استعداد للمغادرة من (.+)\./);

        if (matchStopped) {
            currentStationName = matchStopped[1];
        } else if (matchArriving) {
            const nextStationName = matchArriving[2];
            currentStationName = nextStationName; 
        } else if (matchReady) {
            currentStationName = matchReady[1];
        } else if (statusMessage.includes('وصل القطار')) {
            currentStationName = allStops[allStops.length - 1].stationNameAR;
        }

        if (currentStationName) {
            const currentStopElement = Array.from(stopsListElement.querySelectorAll('li')).find(li => li.dataset.stationName === currentStationName);
            if (currentStopElement) {
                currentStopElement.classList.add('current-stop');
                
                // تم إزالة التمرير التلقائي من هنا ونقله إلى updateTrainPosition لضمان تزامن حركة القطار مع العرض.
            }
        }
    }
    
    /**
     * تبدأ عملية تتبع موقوتة للقطار.
     * @param {Object} tripData - بيانات الرحلة.
     */
    function startTracking(tripData) {
        const tripId = tripData.trip_ID;
        window.stopTracking(tripId); 

        // تحديث الموضع فوراً عند البدء
        updateTrainPosition(tripData); 
        
        // تكرار التحديث كل ثانية لحركة سلسة
        const intervalId = setInterval(() => updateTrainPosition(tripData), 1000); 
        
        // حفظ معرف الـ Interval
        trackingIntervals[tripId] = intervalId;
    }
    
    /**
     * توقف عملية التتبع للقطار.
     * @param {string} tripId - معرف الرحلة.
     */
    window.stopTracking = (tripId) => {
        if (trackingIntervals[tripId]) {
            clearInterval(trackingIntervals[tripId]);
            delete trackingIntervals[tripId];
        }
    };

    /**
     * تعرض النافذة المنبثقة لخريطة المسار وتبدأ التتبع.
     * @param {Object} tripData - بيانات الرحلة.
     */
    function showTripMapModal(tripData) {
        const tripId = tripData.trip_ID;
        
        // 1. تحديث محتوى الـ Modal
        tripMapTitle.textContent = `مسار الرحلة رقم: ${tripId} - ${tripData.trainName.replace('-', '').trim()}`;
        
        // إنشاء محتوى الشريط العمودي والقائمة
        const timelineHtml = createDetailedTimelineHtml(tripData);
        
        tripMapContainer.innerHTML = `
            <div id="tracking-status-${tripId}" class="tracking-status"></div>
            <div class="map-layout">
                <div class="timeline-wrapper" id="timeline-wrapper">
                    ${timelineHtml}
                    <ul class="stops-list-vertical" id="stops-list-${tripId}">
                        </ul>
                </div>
            </div>
        `;
        
        // 2. ملء قائمة المحطات التفصيلية في البنية الجديدة
        renderStopsVertical(tripData, tripId, fromStationName, toStationName);
        
        // 3. عرض الـ Modal
        tripMapModal.style.display = 'flex';
        
        // 4. بدء التتبع
        // تأخير بسيط لضمان حساب ارتفاع العناصر بشكل صحيح بعد ظهور الـ Modal
        setTimeout(() => startTracking(tripData), 100); 
    }
    
    /**
     * دالة جديدة لملء المحطات في الشريط العمودي.
     */
    function renderStopsVertical(tripData, tripId, fromStation, toStation) {
        const stopsListElement = document.getElementById(`stops-list-${tripId}`);
        if (!stopsListElement) return;

        stopsListElement.innerHTML = '';
        const allStops = tripData.stops;
        
        allStops.forEach(stop => {
            const isDeparture = stop.stationNameAR === fromStation;
            const isArrival = stop.stationNameAR === toStation;
            
            let timeInfo = '';
            let stopType = '';
            
            if (isDeparture) {
                stopType = 'محطة الانطلاق';
                timeInfo = `<span class="stop-time">مغادرة: ${formatTime(stop.departureTime)}</span>`;
            } else if (isArrival) {
                stopType = 'محطة الوصول';
                timeInfo = `<span class="stop-time">وصول: ${formatTime(stop.arrivalTime)}</span>`;
            } else {
                stopType = 'توقف';
                timeInfo = `<span class="stop-time">وصول: ${formatTime(stop.arrivalTime)}</span>`;
                if (stop.departureTime && stop.departureTime !== stop.arrivalTime) {
                    timeInfo += ` - <span class="stop-time">مغادرة: ${formatTime(stop.departureTime)}</span>`;
                }
            }

            const listItem = document.createElement('li');
            listItem.dataset.stationName = stop.stationNameAR;
            
            listItem.innerHTML = `
                <strong>${stop.stationNameAR}</strong> (${stopType})
                <br>
                ${timeInfo}
            `;
            stopsListElement.appendChild(listItem);
        });
    }

    /**
     * إنشاء ترميز HTML لشريط المسار العمودي.
     * @param {Object} tripData - بيانات الرحلة.
     * @returns {string} ترميز HTML للشريط.
     */
    function createDetailedTimelineHtml(tripData) {
        return `
            <div class="detailed-timeline" id="detailed-timeline-wrapper">
                <div class="timeline-vertical-track"></div>
                <div id="train-icon-${tripData.trip_ID}" class="train-marker"></div>
            </div>
        `;
    }
    
    /**
     * تُغلق النافذة المنبثقة لخريطة المسار وتوقف التتبع.
     */
    window.closeTripMapModal = () => {
        tripMapModal.style.display = 'none';
        
        // إيقاف التتبع لجميع الرحلات النشطة
        Object.keys(trackingIntervals).forEach(window.stopTracking);
    };


    // ==========================================================
    // ** دوال الموديل لاختيار الدرجة (تم التعديل هنا) **
    // ==========================================================

    /**
     * تُعرض نافذة منبثقة للمستخدم لاختيار درجة الحجز والسعر.
     *
     * **تم تحديث هذه الدالة لاستخراج وتمرير معرفات المحطات**
     * * @param {string} tripId - معرف الرحلة.
     * @param {string} trainName - اسم القطار (للتخزين).
     */
    window.showClassSelectionModal = (tripId, trainName) => {
        const tripData = trips.find(t => t.trip_ID == tripId);

        if (!tripData || !tripData.segmentPrices || tripData.segmentPrices.length === 0) {
            const msg = 'لا تتوفر تفاصيل أسعار لهذه الرحلة حالياً.';
            console.warn(msg);
            return;
        }
        
        // ** التعديل 1: استخراج معرفات محطات الانطلاق والوصول **
        // ملاحظة: نحتاج TripStopID وليس stationID
        const departureStop = tripData.stops.find(s => s.stationNameAR === fromStationName);
        const arrivalStop = tripData.stops.find(s => s.stationNameAR === toStationName);
        
        // استخدام TripStopID بدلاً من stationID (PascalCase كما في C#)
        const departureStopId = departureStop ? (departureStop.tripStopID || departureStop.TripStopID) : 0;
        const arrivalStopId = arrivalStop ? (arrivalStop.tripStopID || arrivalStop.TripStopID) : 0;
        
        // تحويل المعرفات إلى سلاسل نصية لضمان التمرير الصحيح إلى selectClass
        const depIdStr = String(departureStopId);
        const arrIdStr = String(arrivalStopId);
        
        // للتشخيص
        console.log('Selected stops:', {
            fromStation: fromStationName,
            toStation: toStationName,
            departureStop: departureStop,
            arrivalStop: arrivalStop,
            departureStopId: departureStopId,
            arrivalStopId: arrivalStopId
        });
        // ***************************************************************


        classOptionsContainer.innerHTML = '';
        
        tripData.segmentPrices.forEach(priceSegment => {
            const optionDiv = document.createElement('div');
            optionDiv.className = 'class-option';
            
            // ** التعديل 2: تمرير معرفات المحطات الجديدة **
            optionDiv.setAttribute('onclick', `selectClass('${tripId}', ${priceSegment.classID}, '${priceSegment.classNameAR}', ${priceSegment.price}, '${trainName}', 60, '${depIdStr}', '${arrIdStr}')`);
            
            // تعديل البنية الداخلية لتمكين التنسيق الجديد
            optionDiv.innerHTML = `
                <div class="class-info">
                    <div class="class-name">${priceSegment.classNameAR}</div>
                    <div class="class-price">${priceSegment.price} ج.م</div>
                </div>
                <button class="select-class-btn">اختار</button>
            `;
            classOptionsContainer.appendChild(optionDiv);
        });

        modal.style.display = 'flex';
    };

    /**
     * تُغلق النافذة المنبثقة لاختيار الدرجة.
     */
    window.closeClassSelectionModal = () => {
        modal.style.display = 'none';
    };

    /**
     * تُخزن بيانات الدرجة المختارة وتنقل المستخدم لصفحة اختيار المقعد.
     *
     * **تم تحديث هذه الدالة لتخزين معرفات محطات الانطلاق والوصول**
     *
     * @param {string} tripId - معرف الرحلة.
     * @param {number} classId - معرف الدرجة.
     * @param {string} className - اسم الدرجة.
     * @param {number} price - سعر الدرجة.
     * @param {string} trainName - اسم القطار.
     * @param {number} totalSeats - إجمالي المقاعد في العربة.
     * @param {string} departureStopId - معرف محطة الانطلاق للرحلة المحددة.
     * @param {string} arrivalStopId - معرف محطة الوصول للرحلة المحددة.
     */
    window.selectClass = async (tripId, classId, className, price, trainName, totalSeats, departureStopId, arrivalStopId) => {
        // تخزين البيانات الضرورية في localStorage للانتقال لصفحة المقاعد
        localStorage.setItem('selectedTripId', tripId);
        localStorage.setItem('selectedClassId', classId);
        localStorage.setItem('selectedClassName', className);
        localStorage.setItem('selectedClassPrice', price);
        
        // ** تخزين البيانات الجديدة **
        localStorage.setItem('selectedTrainName', trainName);
        // لا نحفظ totalSeats - سيتم تحديده من availableSeatsData
        // ** تخزين معرفات المحطات **
        localStorage.setItem('departureStopId', departureStopId);
        localStorage.setItem('arrivalStopId', arrivalStopId);
        
        closeClassSelectionModal();
        
        // ** جلب المقاعد المتاحة قبل الانتقال للصفحة **
        try {
            const BASE_API_URL = 'https://localhost:7192/api/Booking';
            const url = `${BASE_API_URL}/available-seats?tripId=${tripId}&classId=${classId}&departureStopId=${departureStopId}&arrivalStopId=${arrivalStopId}`;
            
            console.log('Fetching available seats:', url);
            
            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (response.ok) {
                const availableSeatsData = await response.json();
                console.log('Available seats data:', availableSeatsData);
                
                // حفظ بيانات المقاعد المتاحة في localStorage
                localStorage.setItem('availableSeatsData', JSON.stringify(availableSeatsData));
                
                // الانتقال لصفحة اختيار المقاعد
                window.location.href = 'Seat.html';
            } else {
                console.error('Failed to fetch available seats:', response.status);
                alert('فشل في جلب المقاعد المتاحة. سيتم المحاولة مرة أخرى في صفحة المقاعد.');
                // الانتقال حتى في حالة الفشل - ستحاول صفحة المقاعد جلبها مرة أخرى
                window.location.href = 'Seat.html';
            }
        } catch (error) {
            console.error('Error fetching available seats:', error);
            alert('خطأ في الاتصال بالخادم. سيتم المحاولة مرة أخرى في صفحة المقاعد.');
            // الانتقال حتى في حالة الخطأ
            window.location.href = 'Seat.html';
        }
    };

    // دوال مساعدة (formatTime و calculateDuration) تم الاحتفاظ بها كما هي

    function formatTime(timeStr) {
        if (!timeStr) return 'N/A';
        try {
            const [hours, minutes] = timeStr.split(':').map(Number);
            const period = hours >= 12 ? 'مساءً' : 'صباحاً';
            const formattedHours = hours % 12 === 0 ? 12 : hours % 12;
            return `${formattedHours}:${minutes.toString().padStart(2, '0')} ${period}`;
        } catch (e) {
            return timeStr;
        }
    }
    
    function calculateDuration(depTimeStr, arrTimeStr) {
        if (!depTimeStr || !arrTimeStr) return 'غير محددة';
        
        try {
            const today = new Date().toDateString();
            
            const [depH, depM] = depTimeStr.split(':').map(Number);
            const depDate = new Date(`${today} ${depH}:${depM}:00`);

            const [arrH, arrM] = arrTimeStr.split(':').map(Number);
            let arrDate = new Date(`${today} ${arrH}:${arrM}:00`);

            if (arrDate < depDate) {
                arrDate.setDate(arrDate.getDate() + 1);
            }

            const diffMs = arrDate - depDate;
            const totalMinutes = Math.floor(diffMs / (1000 * 60));
            const hours = Math.floor(totalMinutes / 60);
            const minutes = totalMinutes % 60;
            
            let durationStr = '';
            if (hours > 0) {
                durationStr += `${hours} ساعة`;
                if (minutes > 0) {
                    durationStr += ` و `;
                }
            }
            if (minutes > 0) {
                durationStr += `${minutes} دقيقة`;
            }
            
            return durationStr || 'أقل من دقيقة';
            
        } catch (e) {
            console.error("Error calculating duration:", e);
            return 'غير محددة';
        }
    }

});
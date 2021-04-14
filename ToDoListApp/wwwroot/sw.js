const cacheName = 'v2';

function swInstall(event) {
    // Perform install steps
    console.info('Service Worker Installing...');
    event.waitUntil(
        caches.open('v2').then((cache) => {
            return cache.addAll([
                './',
                './home',
                './css/site.css',
                './js/site.js',
                './favicon.ico'
            ]);
        })
    );
    console.info('Finished Caching Website...');
}
self.addEventListener('install', swInstall);

//NetworkFirst
self.addEventListener('fetch', function (event) {
    event.respondWith(
        fetch(event.request)
            .then(function (networkResponse) {
                return networkResponse
            }).catch(function () {
                return caches.match(event.request)
            })
    )
})

//Cache First
//self.addEventListener('fetch', function (event) {
//    event.respondWith(async () => {
//        if (navigator.onLine) {
//            return await fetch(event.request);
//        } else {
//            caches.match(event.request).then(function (cacheResponse) {
//                if (cacheResponse) {
//                    return cacheResponse;
//                } 
//            })
//        }
//    }
//   )
//})

    //.then((netResp) => netResp)
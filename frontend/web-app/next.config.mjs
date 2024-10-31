/** @type {import('next').NextConfig} */
const nextConfig = {
    logging: {
        fetches: {
            fullUrl: true
        }
    },
    images:{
        remotePatterns:[
            {protocol:'https', hostname:'cdn.pixabay.com'},
            {protocol:'https', hostname:'images.unsplash.com'},
            {protocol:'https', hostname:'*'}
        ]
    }
};

export default nextConfig;

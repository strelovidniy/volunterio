const supportedLanguages = [
    "en-US",
    "de-DE",
    "fr-FR",
    "es-ES",
    "it-IT",
    "ja-JP",
    "uk-UA"
]

const handleOnLoad = () => {
    const storedLocale = localStorage.getItem('locale');

    let supportedLanguage;

    const href = window.location.href;
    const origin = window.location.origin;

    let path = href.replace(origin, '');

    if (!path.startsWith('/#')) {
        path = `/#${path}`;
    }

    if (supportedLanguages.some(lang => lang === storedLocale)) {
        supportedLanguage = storedLocale;
    } else {
        let languages = navigator.languages;

        let supportedLanguage = getSupportedLanguage(languages);
    
        supportedLanguage = supportedLanguage || getSupportedLanguage([navigator.language.split('-')[0]]);

        supportedLanguage = supportedLanguage || "en-US";
    }
    
    window.location.href = `/${supportedLanguage}/${path}`;
}

const getSupportedLanguage = (languages) => {
    let supportedLanguage = null;

    languages.forEach((language) => {
        const foundLanguage = supportedLanguages.find(lang => lang.split('-')[0].toLowerCase() == language.split('-')[0].toLowerCase())

        if (!!foundLanguage && !supportedLanguage) {
            supportedLanguage = foundLanguage;

            return;
        }
    });

    return supportedLanguage;
}

handleOnLoad();

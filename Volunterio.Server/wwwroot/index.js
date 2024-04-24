const supportedLanguages = [
    "en-US",
    "de-DE",
    "fr-FR",
    "es-ES",
    "it-IT",
    "ja-JP"
]

const handleOnLoad = () => {
    let languages = navigator.languages;

    let supportedLanguage = getSupportedLanguage(languages);

    const href = window.location.href;
    const orgign = window.location.origin;

    const path = href.replace(orgign, '');

    supportedLanguage = supportedLanguage || getSupportedLanguage([navigator.language.split('-')[0]]);

    supportedLanguage = supportedLanguage || "en-US";

    window.location.href = `/${supportedLanguage}/${path}`;
}

const getSupportedLanguage = (languages) => {
    let supportedLanguage = null;

    languages.forEach((language) => {
        const foundLanguage = supportedLanguages.find(lang => lang.split('-')[0] == language.split('-')[0])

        if (!!foundLanguage && !supportedLanguage) {
            supportedLanguage = foundLanguage;

            return;
        }
    });

    return supportedLanguage;
}

handleOnLoad();

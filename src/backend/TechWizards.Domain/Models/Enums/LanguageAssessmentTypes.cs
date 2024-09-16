using System.ComponentModel;
using Newtonsoft.Json;
using Temporary.Converters;

namespace TechWizards.Domain.Models.Enums;

/// <summary>
/// Defines language assessment types.
/// </summary>
[JsonConverter(typeof(EnumToStringConverter<LanguageAssessmentTypes>))]
public enum LanguageAssessmentTypes
{
    /// <summary>
    /// Refers to an assessment focusing on grammar rules and usage in the language.
    /// </summary>
    [Description("Assessment of language syntax, sentence structure, and grammar rules.")]
    Grammar,

    /// <summary>
    /// Refers to an assessment focusing on the ability to understand spoken language.
    /// </summary>
    [Description("Assessment of listening comprehension, understanding spoken words and phrases.")]
    Listening,

    /// <summary>
    /// Refers to an assessment focusing on the ability to understand written texts.
    /// </summary>
    [Description("Assessment of reading comprehension, interpreting written texts and context.")]
    Reading,

    /// <summary>
    /// Refers to an assessment focusing on the ability to speak and articulate in the language.
    /// </summary>
    [Description("Assessment of verbal communication skills, including pronunciation and fluency.")]
    Speaking,

    /// <summary>
    /// Refers to an assessment focusing on writing skills and the ability to convey ideas in written form.
    /// </summary>
    [Description("Assessment of writing skills, including grammar, coherence, and vocabulary usage.")]
    Writing
}
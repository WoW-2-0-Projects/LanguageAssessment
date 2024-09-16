import { useAssessmentContext } from "@/context/assessment"

export default function QuestionCounter() {
    const context = useAssessmentContext();
    if (!context) throw new Error("Assessment not loaded");

    const { assessment } = context

    return (
        <div style={{padding: "0 0 10px 0", fontWeight: "600", fontSize: "22px"}}>{assessment.currentQuestion + 1} out of {assessment.questions.length}</div>
    )
}
import { Dispatch, SetStateAction } from "react";

export interface AssessmentTest {
    id: string,
    level: number,
    topics: string[],
    questions: Question[],
    currentQuestion: number,
    testType: string,
    passedWarning?: boolean,
    audioFileUrl?: string,
}

export interface Question {
    id: string,
    questionText: string,
    type: number,
    answers: Answer[],
    selectedAnswer?: string,
}

export interface Answer {
    id: string,
    optionText: string
}

export interface AssessmentContext {
    assessment: AssessmentTest,
    setAssessment: Dispatch<SetStateAction<AssessmentTest | null | undefined>>,
}
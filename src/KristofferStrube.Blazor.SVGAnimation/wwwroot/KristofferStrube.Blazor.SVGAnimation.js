export function beginElement(element) {
    element.beginElement();
}

export function endElement(element) {
    element.endElement();
}

export function subscribeToBegin(element, objRef) {
    element.addEventListener("beginEvent", () => objRef.invokeMethod("InvokeOnBegin"));
}

export function subscribeToEnd(element, objRef) {
    element.addEventListener("endEvent", () => objRef.invokeMethod("InvokeOnEnd"));
}

export function subscribeToRepeat(element, objRef) {
    element.addEventListener("repeatEvent", () => objRef.invokeMethod("InvokeOnRepeat"));
}
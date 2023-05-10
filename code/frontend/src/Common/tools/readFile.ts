export function readFile(file: File): Promise<string | null> {
  return new Promise((resolve, reject) => {
    const fr = new FileReader();
    fr.onload = () => {
      resolve(fr.result as string | null);
    };
    fr.onerror = reject;
    fr.readAsText(file);
  });
}

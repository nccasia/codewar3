import { AutoGenerateTestcaseTemplatePage } from './app.po';

describe('AutoGenerateTestcase App', function() {
  let page: AutoGenerateTestcaseTemplatePage;

  beforeEach(() => {
    page = new AutoGenerateTestcaseTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});

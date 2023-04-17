using ClusteringLib;

namespace Кластеризация {

    public class AglomerativeOptionsModel {

        AglomerativeOptionsForm form;

        public ClusteringOptions Options;

        public AglomerativeOptionsModel(AglomerativeOptionsForm form, ClusteringOptions options) {
            this.form = form;
            Options = options;
        }

    }

}